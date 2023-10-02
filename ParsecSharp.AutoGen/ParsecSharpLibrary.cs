using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CppSharp;
using CppSharp.AST;
using CppSharp.AST.Extensions;
using CppSharp.Generators;
using CppSharp.Passes;

namespace ParsecSharp.AutoGen;

public class ParsecSharpLibrary : ILibrary
{
    public void Preprocess(Driver driver, ASTContext ctx)
    {
        ctx.SetClassAsValueType("ParsecConfig");
        ctx.SetClassAsValueType("ParsecClientConfig");
        ctx.SetClassAsValueType("ParsecHostConfig");
        
        ctx.SetClassAsValueType("ParsecFrame");
        ctx.SetClassAsValueType("ParsecCursor");
        ctx.SetClassAsValueType("ParsecPermissions");
        ctx.SetClassAsValueType("ParsecMetrics");
        ctx.SetClassAsValueType("ParsecGuest");
        
        ctx.SetClassAsValueType("ParsecKeyboardMessage");
        ctx.SetClassAsValueType("ParsecMouseButtonMessage");
        ctx.SetClassAsValueType("ParsecMouseWheelMessage");
        ctx.SetClassAsValueType("ParsecMouseMotionMessage");
        ctx.SetClassAsValueType("ParsecGamepadButtonMessage");
        ctx.SetClassAsValueType("ParsecGamepadAxisMessage");
        ctx.SetClassAsValueType("ParsecGamepadUnplugMessage");
        ctx.SetClassAsValueType("ParsecMessage");
        
        ctx.SetClassAsValueType("ParsecClientStatus");
        ctx.SetClassAsValueType("ParsecClientCursorEvent");
        ctx.SetClassAsValueType("ParsecClientRumbleEvent");
        ctx.SetClassAsValueType("ParsecClientUserDataEvent");
        ctx.SetClassAsValueType("ParsecClientEvent");
        ctx.SetClassAsValueType("ParsecHostStatus");
        ctx.SetClassAsValueType("ParsecGuestStateChangeEvent");
        ctx.SetClassAsValueType("ParsecUserDataEvent");
        ctx.SetClassAsValueType("ParsecHostEvent");
        
        ctx.SetEnumAsFlags("ParsecGuestState");
        
        driver.AddTranslationUnitPass(new RegexRenamePass("^Parsec(?!$)", ""));
        driver.AddTranslationUnitPass(new MoveFunctionsToInstanceMethodPass());
        driver.AddTranslationUnitPass(new MoveFunctionsToParsecMethodPass());
        driver.AddTranslationUnitPass(new RemoveCopyConstructorsPass());
        driver.AddTranslationUnitPass(new CustomModifierPass());
        driver.AddTranslationUnitPass(new RenameEnumPass());
    }

    public void Postprocess(Driver driver, ASTContext ctx)
    { }

    public void Setup(Driver driver)
    {
        var options = driver.Options;
        options.GeneratorKind = GeneratorKind.CSharp;
        var module = options.AddModule(nameof(ParsecSharp));
        module.IncludeDirs.Add(@"..\sdk");
        module.Headers.Add("parsec.h");
        module.LibraryDirs.Add(@"..\sdk\windows");
        module.Libraries.Add("parsec.lib");
        module.OutputNamespace = "ParsecSharp";
        module.LibraryName = "ParsecSharp.g";
    }

    public void SetupPasses(Driver driver)
    { }
    
    /// <summary>
    /// 将函数转换为实例方法
    /// </summary>
    /// <remarks>
    /// 原方法：
    /// public static void GetConfig(global::ParsecSharp.Parsec ps, global::ParsecSharp.Config cfg)
    /// 转换后：
    /// public void global::ParsecSharp.Parsec::GetConfig(global::ParsecSharp.Config cfg)
    /// </remarks>
    private class MoveFunctionsToInstanceMethodPass : TranslationUnitPass
    {
        public override bool VisitMethodDecl(Method method)
        {
            return true;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (AlreadyVisited(function)) return false;
            if (!function.IsGenerated) return false;
            if (function.Parameters.Count == 0) return false;

            var classParam = function.Parameters[0];
            if (!GetClassParameter(classParam, out var @class)) return false;

            // If we reach here, it means the first parameter is of class type.
            // This means we can change the function to be an instance method.

            function.ExplicitlyIgnore();

            // Create a new fake method so it acts as an instance method.
            var method = new Method
            {
                Namespace = @class,
                OriginalNamespace = function.Namespace,
                Name = function.Name,
                OriginalName = function.OriginalName,
                Mangled = function.Mangled,
                Access = AccessSpecifier.Public,
                Kind = CXXMethodKind.Normal,
                ReturnType = function.ReturnType,
                CallingConvention = function.CallingConvention,
                IsVariadic = function.IsVariadic,
                IsInline = function.IsInline,
                Conversion = MethodConversionKind.FunctionToInstanceMethod,
                FunctionType = function.FunctionType
            };

            method.Parameters.AddRange(function.Parameters.Select(
                p => new Parameter(p) { Namespace = method }));

            if (Options.GeneratorKind == GeneratorKind.CSharp)
                method.Parameters.RemoveAt(0);

            @class.Methods.Add(method);

            Diagnostics.Debug("Function converted to instance method: {0}::{1}", @class.Name,
                function.Name);

            return true;
        }

        private static bool GetClassParameter(Parameter classParam, [NotNullWhen(true)] out Class? @class)
        {
            if (classParam.Type.IsPointerTo(out TagType tag))
            {
                @class = tag.Declaration as Class;
                return @class != null;
            }
            
            if (classParam.Type.IsPointerTo(out TypedefType typedef))
            {
                return typedef.TryGetClass(out @class);
            }

            return classParam.Type.TryGetClass(out @class);
        }
    }
    
    private class MoveFunctionsToParsecMethodPass : TranslationUnitPass
    {
        public override bool VisitFunctionDecl(Function function)
        {
            if (AlreadyVisited(function)) return false;
            if (!function.IsGenerated) return false;
            if (function.Namespace.ToString() != "parsec.h") return false;
            
            var parsecClass = function.TranslationUnit.Classes.First(c => c.Name == "Parsec");
            var method = new Method
            {
                Namespace = parsecClass,
                OriginalNamespace = function.Namespace,
                Name = function.Name,
                OriginalName = function.OriginalName,
                Mangled = function.Mangled,
                Access = AccessSpecifier.Public,
                Kind = CXXMethodKind.Normal,
                ReturnType = function.ReturnType,
                CallingConvention = function.CallingConvention,
                IsVariadic = function.IsVariadic,
                IsInline = function.IsInline,
                Conversion = MethodConversionKind.FunctionToInstanceMethod,
                FunctionType = function.FunctionType,
                IsStatic = true
            };
            method.Parameters.AddRange(function.Parameters.Select(
                p => new Parameter(p) { Namespace = method }));
            parsecClass.Methods.Add(method);
            function.ExplicitlyIgnore();
            
            return base.VisitFunctionDecl(function);
        }
    }
    
    private class RemoveCopyConstructorsPass : TranslationUnitPass
    {
        public override bool VisitMethodDecl(Method method)
        {
            // 如果这个方法是一个构造函数并且只有一个参数
            if (!method.IsConstructor || method.Parameters.Count != 1) return false;
            // 如果参数类型与所在的类相同
            if (method.Parameters[0].Type.ToString() != method.Type.ToString()) return false;
            // 删除这个构造函数
            method.ExplicitlyIgnore();
            return true;
        }
    }

    private class CustomModifierPass : TranslationUnitPass
    {
        public override bool VisitMethodDecl(Method method)
        {
            if (AlreadyVisited(method)) return false;
            if (!method.IsGenerated) return false;
            
            if (method.QualifiedName is "Parsec::Init" or "Parsec::Destroy")
            {
                method.Access = AccessSpecifier.Internal;
            }
            
            var modified = false;

            foreach (var parameter in method.Parameters)
            {
                var pointee = parameter.Type.GetPointee();
                if (pointee is PointerType)
                {
                    // 多指针
                    parameter.QualifiedType = new QualifiedType(new BuiltinType(PrimitiveType.IntPtr));
                }
            }

            return modified || base.VisitMethodDecl(method);
        }
    }

    private class RenameEnumPass : TranslationUnitPass
    {
        public override bool VisitEnumDecl(Enumeration @enum)
        {
            if (AlreadyVisited(@enum)) return false;
            if (!@enum.IsGenerated) return false;
            
            foreach (var item in @enum.Items)
            {
                if (item.Name.EndsWith("_MAKE_32"))
                {
                    item.ExplicitlyIgnore();
                }
                
                item.Name = ConvertToUpperCamelCase(item.Name);
            }

            var samePrefixLength = 0;
            while (true)
            {
                var samePrefix = true;
                char prefix = default;
                foreach (var item in @enum.Items)
                {
                    if (samePrefixLength >= item.Name.Length)
                    {
                        samePrefix = false;
                        break;
                    }
                    
                    if (prefix == default)
                    {
                        prefix = item.Name[samePrefixLength];
                    }
                    else if (prefix != item.Name[samePrefixLength])
                    {
                        samePrefix = false;
                        break;
                    }
                }
                
                if (!samePrefix)
                {
                    break;
                }
                
                samePrefixLength++;
            }
            
            foreach (var item in @enum.Items)
            {
                item.Name = item.Name[samePrefixLength..];
                
                if (@enum.Name == "Keycode" && item.Name[0] is <= '9' and >= '0')
                {
                    item.Name = $"Digital{item.Name}";
                }
            }
            
            return base.VisitEnumDecl(@enum);
        }
        
        private readonly Dictionary<string, string> specialMapping = new()
        {
            // ReSharper disable StringLiteralTypo
            { "WRN", "Warn" },
            { "ERR", "Error" },
            { "WS", "Websocket" },
            { "OPENGL", "OpenGl" },
            { "KP", "NumPad" },
            { "LBRACKET", "LeftBracket" },
            { "RBRACKET", "RightBracket" },
            { "CAPSLOCK", "CapsLock" },
            { "PRINTSCREEN", "PrintScreen" },
            { "SCROLLLOCK", "ScrollLock" },
            { "PAGEUP", "PageUp" },
            { "PAGEDOWN", "PageDown" },
            { "NUMLOCK", "NumLock" },
            { "LCTRL", "LeftCtrl" },
            { "LSHIFT", "LeftShift" },
            { "LALT", "LeftAlt" },
            { "LGUI", "LeftGui" },
            { "RCTRL", "RightCtrl" },
            { "RSHIFT", "RightShift" },
            { "RALT", "RightAlt" },
            { "RGUI", "RightGui" },
            { "VOLUMEUP", "VolumeUp" },
            { "VOLUMEDOWN", "VolumeDown" },
            { "AUDIONEXT", "AudioNext" },
            { "AUDIOPREV", "AudioPrevious" },
            { "AUDIOPLAY", "AudioPlay" },
            { "AUDIOSTOP", "AudioStop" },
            { "AUDIOMUTE", "AudioMute" },
            { "MEDIASELECT", "MediaSelect" },
            { "LSTICK", "LeftStick" },
            { "RSTICK", "RightStick" },
            { "LSHOULDER", "LeftShoulder" },
            { "RSHOULDER", "RightShoulder" },
            { "LX", "LeftX" },
            { "LY", "LeftY" },
            { "RX", "RightX" },
            { "RY", "RightY" },
            { "TRIGGERL", "LeftTrigger" },
            { "TRIGGERR", "RightTrigger" },
            // ReSharper restore StringLiteralTypo
        };

        private string ConvertToUpperCamelCase(string input)
        {
            return string.Join("",
                input.Split('_')
                    .Select(word => 
                        specialMapping.TryGetValue(word, out var value) ? 
                            value : 
                            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()))
            );
        }
    }
}