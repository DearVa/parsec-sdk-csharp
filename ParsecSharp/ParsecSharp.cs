using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Security;
using CppSharp.Runtime;

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required

namespace ParsecSharp
{
	/// <summary>Status codes indicating success, warning, or error.</summary>
	/// <remarks>
	///     <para>Returned by most Parsec SDK functions. ::PARSEC_OK is `0`,</para>
	///     <para>warnings are positive, errors are negative.</para>
	/// </remarks>
	public enum Status
	{
		/// <summary>0</summary>
		ParsecOk = 0,
		/// <summary>10</summary>
		WarnContinue = 10,
		/// <summary>4</summary>
		HostWarnShutdown = 4,
		/// <summary>5</summary>
		HostWarnKicked = 5,
		/// <summary>6</summary>
		ConnectWarnApproval = 6,
		/// <summary>8</summary>
		ConnectWarnDeclined = 8,
		/// <summary>9</summary>
		ConnectWarnCanceled = 9,
		/// <summary>99</summary>
		ConnectWarnPeerGone = 99,
		/// <summary>1000</summary>
		DecodeWarnContinue = 1000,
		/// <summary>1001</summary>
		DecodeWarnAccepted = 1001,
		/// <summary>1003</summary>
		DecodeWarnReinit = 1003,
		/// <summary>2000</summary>
		NetworkWarnTimeout = 2000,
		/// <summary>5000</summary>
		QueueWarnEmpty = 5000,
		/// <summary>5001</summary>
		QueueWarnNoBuffer = 5001,
		/// <summary>5003</summary>
		QueueWarnTimeout = 5003,
		/// <summary>6000</summary>
		AudioWarnNoData = 6000,
		/// <summary>-1</summary>
		ErrorDefault = -1,
		/// <summary>-3</summary>
		ParsecNotRunning = -3,
		/// <summary>-4</summary>
		ParsecAlreadyRunning = -4,
		/// <summary>-5</summary>
		ParsecNotImplemented = -5,
		/// <summary>-10</summary>
		DecodeErrorInit = -10,
		/// <summary>-11</summary>
		DecodeErrorLoad = -11,
		/// <summary>-13</summary>
		DecodeErrorMap = -13,
		/// <summary>-14</summary>
		DecodeErrorDecode = -14,
		/// <summary>-15</summary>
		DecodeErrorCleanup = -15,
		/// <summary>-16</summary>
		DecodeErrorParse = -16,
		/// <summary>-17</summary>
		DecodeErrorNoSupport = -17,
		/// <summary>-18</summary>
		DecodeErrorPixelFormat = -18,
		/// <summary>-19</summary>
		DecodeErrorBuffer = -19,
		/// <summary>-20</summary>
		DecodeErrorResolution = -20,
		/// <summary>-6101</summary>
		WebsocketErrorConnect = -6101,
		/// <summary>-3001</summary>
		WebsocketErrorPoll = -3001,
		/// <summary>-3002</summary>
		WebsocketErrorRead = -3002,
		/// <summary>-3003</summary>
		WebsocketErrorWrite = -3003,
		/// <summary>-6105</summary>
		WebsocketErrorClose = -6105,
		/// <summary>-3005</summary>
		WebsocketErrorPing = -3005,
		/// <summary>-3006</summary>
		WebsocketErrorPongTimeout = -3006,
		/// <summary>-3007</summary>
		WebsocketErrorPong = -3007,
		/// <summary>-3008</summary>
		WebsocketErrorAuth = -3008,
		/// <summary>-3009</summary>
		WebsocketErrorGoingAway = -3009,
		/// <summary>-5000</summary>
		ZlibErrorDeflate = -5000,
		/// <summary>-5001</summary>
		ZlibErrorInflate = -5001,
		/// <summary>-6023</summary>
		NatErrorPeerPhase = -6023,
		/// <summary>-6024</summary>
		NatErrorStunPhase = -6024,
		/// <summary>-6033</summary>
		NatErrorNoCandidates = -6033,
		/// <summary>-6111</summary>
		NatErrorJsonAction = -6111,
		/// <summary>-6112</summary>
		NatErrorNoSocket = -6112,
		/// <summary>-7000</summary>
		OpenGLErrorContext = -7000,
		/// <summary>-7001</summary>
		OpenGlErrorShare = -7001,
		/// <summary>-7002</summary>
		OpenGlErrorPixelFormat = -7002,
		/// <summary>-7003</summary>
		OpenGlErrorCurrent = -7003,
		/// <summary>-7004</summary>
		OpenGlErrorDc = -7004,
		/// <summary>-7005</summary>
		OpenGlErrorShader = -7005,
		/// <summary>-7006</summary>
		OpenGlErrorProgram = -7006,
		/// <summary>-7007</summary>
		OpenGlErrorVersion = -7007,
		/// <summary>-7008</summary>
		OpenGlErrorTexture = -7008,
		/// <summary>-8000</summary>
		JsonErrorParse = -8000,
		/// <summary>-8001</summary>
		JsonErrorMissing = -8001,
		/// <summary>-8002</summary>
		JsonErrorType = -8002,
		/// <summary>-8003</summary>
		JsonErrorValType = -8003,
		/// <summary>-8004</summary>
		JsonErrorBuffer = -8004,
		/// <summary>-8005</summary>
		JsonErrorFileOpen = -8005,
		/// <summary>-8006</summary>
		JsonErrorFileRead = -8006,
		/// <summary>-8007</summary>
		JsonErrorFileWrite = -8007,
		/// <summary>-9000</summary>
		AudioErrorInit = -9000,
		/// <summary>-9001</summary>
		AudioErrorCapture = -9001,
		/// <summary>-9002</summary>
		AudioErrorNetwork = -9002,
		/// <summary>-9003</summary>
		AudioErrorFree = -9003,
		/// <summary>-9004</summary>
		AudioErrorPlay = -9004,
		/// <summary>-10000</summary>
		AudioOpusErrorInit = -10000,
		/// <summary>-10001</summary>
		AudioOpusErrorDecode = -10001,
		/// <summary>-10002</summary>
		AudioOpusErrorEncode = -10002,
		/// <summary>-12007</summary>
		NetworkErrorBgTimeout = -12007,
		/// <summary>-12008</summary>
		NetworkErrorBadPacket = -12008,
		/// <summary>-12011</summary>
		NetworkErrorBuffer = -12011,
		/// <summary>-12017</summary>
		NetworkErrorShutdown = -12017,
		/// <summary>-12018</summary>
		NetworkErrorUnsupported = -12018,
		/// <summary>-12019</summary>
		NetworkErrorInterrupted = -12019,
		/// <summary>-13000</summary>
		ServerErrorDisplay = -13000,
		/// <summary>-13008</summary>
		ServerErrorResolution = -13008,
		/// <summary>-13009</summary>
		ServerErrorMaxResolution = -13009,
		/// <summary>-13011</summary>
		ServerErrorNoUser = -13011,
		/// <summary>-13012</summary>
		ServerErrorNoRoom = -13012,
		/// <summary>-13013</summary>
		ServerErrorVideoDone = -13013,
		/// <summary>-13014</summary>
		ServerErrorClientAbort = -13014,
		/// <summary>-13015</summary>
		ServerErrorClientGone = -13015,
		/// <summary>-14003</summary>
		CaptureErrorInit = -14003,
		/// <summary>-14004</summary>
		CaptureErrorTexture = -14004,
		/// <summary>-15000</summary>
		EncodeErrorInit = -15000,
		/// <summary>-15002</summary>
		EncodeErrorEncode = -15002,
		/// <summary>-15006</summary>
		EncodeErrorBuffer = -15006,
		/// <summary>-15100</summary>
		EncodeErrorProperties = -15100,
		/// <summary>-15101</summary>
		EncodeErrorLibrary = -15101,
		/// <summary>-15007</summary>
		EncodeErrorSession = -15007,
		/// <summary>-15103</summary>
		EncodeErrorSession1 = -15103,
		/// <summary>-15104</summary>
		EncodeErrorSession2 = -15104,
		/// <summary>-15105</summary>
		EncodeErrorOutputInit = -15105,
		/// <summary>-15106</summary>
		EncodeErrorTexture = -15106,
		/// <summary>-15107</summary>
		EncodeErrorOutput = -15107,
		/// <summary>-15108</summary>
		EncodeErrorUnsupported = -15108,
		/// <summary>-15109</summary>
		EncodeErrorHandle = -15109,
		/// <summary>-15110</summary>
		EncodeErrorCaps = -15110,
		/// <summary>-19000</summary>
		UpnpError = -19000,
		/// <summary>-22000</summary>
		D3dErrorTexture = -22000,
		/// <summary>-22001</summary>
		D3dErrorShader = -22001,
		/// <summary>-22002</summary>
		D3dErrorBuffer = -22002,
		/// <summary>-22003</summary>
		D3dErrorLayout = -22003,
		/// <summary>-22004</summary>
		D3dErrorDevice = -22004,
		/// <summary>-22005</summary>
		D3dErrorMt = -22005,
		/// <summary>-22006</summary>
		D3dErrorAdapter = -22006,
		/// <summary>-22007</summary>
		D3dErrorFactory = -22007,
		/// <summary>-22008</summary>
		D3dErrorOutput = -22008,
		/// <summary>-22009</summary>
		D3dErrorContext = -22009,
		/// <summary>-22010</summary>
		D3dErrorOutput1 = -22010,
		/// <summary>-22011</summary>
		D3dErrorSwapChain = -22011,
		/// <summary>-22012</summary>
		D3dErrorDraw = -22012,
		/// <summary>-22013</summary>
		D3dErrorOutput5 = -22013,
		/// <summary>-23000</summary>
		H26XErrorNotFound = -23000,
		/// <summary>-28000</summary>
		AesGcmErrorKeyLen = -28000,
		/// <summary>-28001</summary>
		AesGcmErrorEncrypt = -28001,
		/// <summary>-28002</summary>
		AesGcmErrorDecrypt = -28002,
		/// <summary>-28003</summary>
		AesGcmErrorCtx = -28003,
		/// <summary>-28004</summary>
		AesGcmErrorBuffer = -28004,
		/// <summary>-28005</summary>
		AesGcmErrorOverflow = -28005,
		/// <summary>-32000</summary>
		SctpErrorGlobalInit = -32000,
		/// <summary>-32001</summary>
		SctpErrorWrite = -32001,
		/// <summary>-32002</summary>
		SctpErrorSocket = -32002,
		/// <summary>-32003</summary>
		SctpErrorBind = -32003,
		/// <summary>-32004</summary>
		SctpErrorConnect = -32004,
		/// <summary>-33000</summary>
		DtlsErrorBioWrite = -33000,
		/// <summary>-33001</summary>
		DtlsErrorBioRead = -33001,
		/// <summary>-33002</summary>
		DtlsErrorSsl = -33002,
		/// <summary>-33003</summary>
		DtlsErrorBuffer = -33003,
		/// <summary>-33004</summary>
		DtlsErrorNoData = -33004,
		/// <summary>-33005</summary>
		DtlsErrorCert = -33005,
		/// <summary>-34000</summary>
		StunErrorPacket = -34000,
		/// <summary>-34001</summary>
		StunErrorParseHeader = -34001,
		/// <summary>-34002</summary>
		StunErrorParseAddress = -34002,
		/// <summary>-35000</summary>
		SoErrorOpen = -35000,
		/// <summary>-35001</summary>
		SoErrorSymbol = -35001,
		/// <summary>-36000</summary>
		ParsecErrorVersion = -36000,
		/// <summary>-36001</summary>
		ParsecErrorVerData = -36001,
		/// <summary>-37000</summary>
		ResampleErrorInit = -37000,
		/// <summary>-37001</summary>
		ResampleErrorResample = -37001,
		/// <summary>Caused when the graphics render engine isn't supported in Unity.</summary>
		UnityUnsupportedEngine = -38000,
		/// <summary>`SSL_get_error` value will be subtracted from this value.</summary>
		OpensslError = -600000,
		/// <summary>`WSAGetLastError` value will be subtracted from this value.</summary>
		SocketError = -700000,
	}

	/// <summary>Log level.</summary>
	/// <remarks>Passed through ::ParsecLogCallback set with ::ParsecSetLogCallback.</remarks>
	public enum LogLevel
	{
		/// <summary>Messages interesting to support staff trying to figure out the context of an issue.</summary>
		Info = 105,
		/// <summary>Messages interesting to developers trying to debug an issue.</summary>
		Debug = 100,
	}

	/// <summary>Keyboard input.</summary>
	/// <remarks>Member of ::ParsecKeyboardMessage.</remarks>
	public enum Keycode
	{
		/// <summary>4</summary>
		A = 4,
		/// <summary>5</summary>
		B = 5,
		/// <summary>6</summary>
		C = 6,
		/// <summary>7</summary>
		D = 7,
		/// <summary>8</summary>
		E = 8,
		/// <summary>9</summary>
		F = 9,
		/// <summary>10</summary>
		G = 10,
		/// <summary>11</summary>
		H = 11,
		/// <summary>12</summary>
		I = 12,
		/// <summary>13</summary>
		J = 13,
		/// <summary>14</summary>
		K = 14,
		/// <summary>15</summary>
		L = 15,
		/// <summary>16</summary>
		M = 16,
		/// <summary>17</summary>
		N = 17,
		/// <summary>18</summary>
		O = 18,
		/// <summary>19</summary>
		P = 19,
		/// <summary>20</summary>
		Q = 20,
		/// <summary>21</summary>
		R = 21,
		/// <summary>22</summary>
		S = 22,
		/// <summary>23</summary>
		T = 23,
		/// <summary>24</summary>
		U = 24,
		/// <summary>25</summary>
		V = 25,
		/// <summary>26</summary>
		W = 26,
		/// <summary>27</summary>
		X = 27,
		/// <summary>28</summary>
		Y = 28,
		/// <summary>29</summary>
		Z = 29,
		/// <summary>30</summary>
		Digital1 = 30,
		/// <summary>31</summary>
		Digital2 = 31,
		/// <summary>32</summary>
		Digital3 = 32,
		/// <summary>33</summary>
		Digital4 = 33,
		/// <summary>34</summary>
		Digital5 = 34,
		/// <summary>35</summary>
		Digital6 = 35,
		/// <summary>36</summary>
		Digital7 = 36,
		/// <summary>37</summary>
		Digital8 = 37,
		/// <summary>38</summary>
		Digital9 = 38,
		/// <summary>39</summary>
		Digital0 = 39,
		/// <summary>40</summary>
		Enter = 40,
		/// <summary>41</summary>
		Escape = 41,
		/// <summary>42</summary>
		Backspace = 42,
		/// <summary>43</summary>
		Tab = 43,
		/// <summary>44</summary>
		Space = 44,
		/// <summary>45</summary>
		Minus = 45,
		/// <summary>46</summary>
		Equals = 46,
		/// <summary>47</summary>
		LeftBracket = 47,
		/// <summary>48</summary>
		RightBracket = 48,
		/// <summary>49</summary>
		Backslash = 49,
		/// <summary>51</summary>
		Semicolon = 51,
		/// <summary>52</summary>
		Apostrophe = 52,
		/// <summary>53</summary>
		Backtick = 53,
		/// <summary>54</summary>
		Comma = 54,
		/// <summary>55</summary>
		Period = 55,
		/// <summary>56</summary>
		Slash = 56,
		/// <summary>57</summary>
		CapsLock = 57,
		/// <summary>58</summary>
		F1 = 58,
		/// <summary>59</summary>
		F2 = 59,
		/// <summary>60</summary>
		F3 = 60,
		/// <summary>61</summary>
		F4 = 61,
		/// <summary>62</summary>
		F5 = 62,
		/// <summary>63</summary>
		F6 = 63,
		/// <summary>64</summary>
		F7 = 64,
		/// <summary>65</summary>
		F8 = 65,
		/// <summary>66</summary>
		F9 = 66,
		/// <summary>67</summary>
		F10 = 67,
		/// <summary>68</summary>
		F11 = 68,
		/// <summary>69</summary>
		F12 = 69,
		/// <summary>70</summary>
		PrintScreen = 70,
		/// <summary>71</summary>
		ScrollLock = 71,
		/// <summary>72</summary>
		Pause = 72,
		/// <summary>73</summary>
		Insert = 73,
		/// <summary>74</summary>
		Home = 74,
		/// <summary>75</summary>
		PageUp = 75,
		/// <summary>76</summary>
		Delete = 76,
		/// <summary>77</summary>
		End = 77,
		/// <summary>78</summary>
		PageDown = 78,
		/// <summary>79</summary>
		Right = 79,
		/// <summary>80</summary>
		Left = 80,
		/// <summary>81</summary>
		Down = 81,
		/// <summary>82</summary>
		Up = 82,
		/// <summary>83</summary>
		NumLock = 83,
		/// <summary>84</summary>
		NumPadDivide = 84,
		/// <summary>85</summary>
		NumPadMultiply = 85,
		/// <summary>86</summary>
		NumPadMinus = 86,
		/// <summary>87</summary>
		NumPadPlus = 87,
		/// <summary>88</summary>
		NumPadEnter = 88,
		/// <summary>89</summary>
		NumPad1 = 89,
		/// <summary>90</summary>
		NumPad2 = 90,
		/// <summary>91</summary>
		NumPad3 = 91,
		/// <summary>92</summary>
		NumPad4 = 92,
		/// <summary>93</summary>
		NumPad5 = 93,
		/// <summary>94</summary>
		NumPad6 = 94,
		/// <summary>95</summary>
		NumPad7 = 95,
		/// <summary>96</summary>
		NumPad8 = 96,
		/// <summary>97</summary>
		NumPad9 = 97,
		/// <summary>98</summary>
		NumPad0 = 98,
		/// <summary>99</summary>
		NumPadPeriod = 99,
		/// <summary>101</summary>
		Application = 101,
		/// <summary>104</summary>
		F13 = 104,
		/// <summary>105</summary>
		F14 = 105,
		/// <summary>106</summary>
		F15 = 106,
		/// <summary>107</summary>
		F16 = 107,
		/// <summary>108</summary>
		F17 = 108,
		/// <summary>109</summary>
		F18 = 109,
		/// <summary>110</summary>
		F19 = 110,
		/// <summary>118</summary>
		Menu = 118,
		/// <summary>127</summary>
		Mute = 127,
		/// <summary>128</summary>
		VolumeUp = 128,
		/// <summary>129</summary>
		VolumeDown = 129,
		/// <summary>224</summary>
		LeftCtrl = 224,
		/// <summary>225</summary>
		LeftShift = 225,
		/// <summary>226</summary>
		LeftAlt = 226,
		/// <summary>227</summary>
		LeftGui = 227,
		/// <summary>228</summary>
		RightCtrl = 228,
		/// <summary>229</summary>
		RightShift = 229,
		/// <summary>230</summary>
		RightAlt = 230,
		/// <summary>231</summary>
		RightGui = 231,
		/// <summary>258</summary>
		AudioNext = 258,
		/// <summary>259</summary>
		AudioPrevious = 259,
		/// <summary>260</summary>
		AudioStop = 260,
		/// <summary>261</summary>
		AudioPlay = 261,
		/// <summary>262</summary>
		AudioMute = 262,
		/// <summary>263</summary>
		MediaSelect = 263,
	}

	/// <summary>Stateful modifier keys applied to keyboard input.</summary>
	/// <remarks>Member of ::ParsecKeyboardMessage. These values may be bitwise OR'd together.</remarks>
	[Flags]
	public enum KeyModifier
	{
		/// <summary>No stateful modifier key active.</summary>
		None = 0,
		/// <summary>`NUMLOCK` is currently active.</summary>
		Num = 4096,
		/// <summary>`CAPSLOCK` is currently active.</summary>
		Caps = 8192,
	}

	/// <summary>Mouse button.</summary>
	/// <remarks>Member of ::ParsecMouseButtonMessage.</remarks>
	public enum MouseButton
	{
		/// <summary>Left mouse button.</summary>
		L = 1,
		/// <summary>Middle mouse button.</summary>
		Middle = 2,
		/// <summary>Right mouse button.</summary>
		R = 3,
		/// <summary>Extra mouse button 1.</summary>
		X1 = 4,
		/// <summary>Extra mouse button 2.</summary>
		X2 = 5,
	}

	/// <summary>Gamepad button.</summary>
	/// <remarks>Member of ::ParsecGamepadButtonMessage.</remarks>
	public enum GamepadButton
	{
		/// <summary>A button.</summary>
		A = 0,
		/// <summary>B button.</summary>
		B = 1,
		/// <summary>X button.</summary>
		X = 2,
		/// <summary>Y button.</summary>
		Y = 3,
		/// <summary>Back button.</summary>
		Back = 4,
		/// <summary>Guide button.</summary>
		Guide = 5,
		/// <summary>Start button.</summary>
		Start = 6,
		/// <summary>Left thumbstick button.</summary>
		LeftStick = 7,
		/// <summary>Right thumbstick button.</summary>
		RightStick = 8,
		/// <summary>Left shoulder (bumper) button.</summary>
		LeftShoulder = 9,
		/// <summary>Right shoulder (bumper) button.</summary>
		RightShoulder = 10,
		/// <summary>Analog DPAD up.</summary>
		DpadUp = 11,
		/// <summary>Analog DPAD down.</summary>
		DpadDown = 12,
		/// <summary>Analog DPAD left.</summary>
		DpadLeft = 13,
		/// <summary>Analog DPAD right.</summary>
		DpadRight = 14,
	}

	/// <summary>Gamepad axes related to thumbsticks and triggers.</summary>
	/// <remarks>Member of ::ParsecGamepadAxisMessage.</remarks>
	public enum GamepadAxis
	{
		/// <summary>Gamepad left thumbstick x-axis.</summary>
		LeftX = 0,
		/// <summary>Gamepad left thumbstick y-axis.</summary>
		LeftY = 1,
		/// <summary>Gamepad right thumbstick x-axis.</summary>
		RightX = 2,
		/// <summary>Gamepad right thumbstick y-axis.</summary>
		RightY = 3,
		/// <summary>Gamepad left trigger value.</summary>
		LeftTrigger = 4,
		/// <summary>Gamepad right trigger value.</summary>
		RightTrigger = 5,
	}

	/// <summary>Input message type.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public enum MessageType
	{
		/// <summary>`keyboard` ::ParsecKeyboardMessage is valid in ::ParsecMessage.</summary>
		Keyboard = 1,
		/// <summary>`mouseButton` ::ParsecMouseButtonMessage is valid in ::ParsecMessage.</summary>
		MouseButton = 2,
		/// <summary>`mouseWheel` ::ParsecMouseWheelMessage is valid in ::ParsecMessage.</summary>
		MouseWheel = 3,
		/// <summary>`mouseMotion` ::ParsecMouseMotionMessage is valid in ::ParsecMessage.</summary>
		MouseMotion = 4,
		/// <summary>`gamepadButton` ::ParsecGamepadButtonMessage is valid in ::ParsecMessage.</summary>
		GamepadButton = 5,
		/// <summary>`gamepadAxis` ::ParsecGamepadAxisMessage is valid in ::ParsecMessage.</summary>
		GamepadAxis = 6,
		/// <summary>`gamepadUnplug` ::ParsecGamepadUnplugMessage is valid in ::ParsecMessage.</summary>
		GamepadUnplug = 7,
	}

	/// <summary>Color formats for raw image data.</summary>
	/// <remarks>Member of ::ParsecFrame.</remarks>
	public enum ColorFormat
	{
		Unknown = 0,
		/// <summary>4:2:0 full width/height Y plane followed by an interleaved half width/height UV plane.</summary>
		Nv12 = 1,
		/// <summary>
		///     4:2:0 full width/height Y plane followed by a half width/height U plane followed by a half width/height V
		///     plane.
		/// </summary>
		I420 = 2,
		/// <summary>4:2:2 full width/height Y plane followed by an interleaved half width full height UV plane.</summary>
		Nv16 = 3,
		/// <summary>
		///     4:2:2 full width/height Y plane followed by a half width full height U plane followed by a half width full
		///     height V plane.
		/// </summary>
		I422 = 4,
		/// <summary>32-bits per pixel, 8-bits per channel BGRA.</summary>
		Bgra = 5,
		/// <summary>32-bits per pixel, 8-bits per channel RGBA.</summary>
		Rgba = 6,
	}

	/// <summary>Network protocol used for peer-to-peer connections.</summary>
	/// <remarks>Member of ::ParsecClientConfig.</remarks>
	public enum Protocol
	{
		/// <summary>Parsec's low-latency optimized BUD protocol.</summary>
		Bud = 1,
		/// <summary>SCTP protocol compatible with WebRTC data channels.</summary>
		Sctp = 2,
	}

	/// <summary>Video stream container.</summary>
	/// <remarks>Member of ::ParsecClientConfig.</remarks>
	public enum Container
	{
		/// <summary>Parsec's custom container compatible with native decoding.</summary>
		Parsec = 0,
		/// <summary>MP4 box container compatible with web browser Media Source Extensions.</summary>
		Mp4 = 2,
	}

	/// <summary>PCM audio format.</summary>
	/// <remarks>Passed to ::ParsecHostSubmitAudio.</remarks>
	public enum PcmFormat
	{
		/// <summary>32-bit floating point samples.</summary>
		Float = 1,
		/// <summary>16-bit signed integer samples.</summary>
		Int16 = 2,
	}

	/// <summary>Guest connection lifecycle states.</summary>
	/// <remarks>Member of ::ParsecGuest and passed to ::ParsecHostGetGuests.</remarks>
	[Flags]
	public enum GuestState
	{
		/// <summary>The guest is currently waiting for the host to allow them via ::ParsecHostAllowGuest. ::HOST_DESKTOP only.</summary>
		Waiting = 1,
		/// <summary>The guest is attempting to make a peer-to-peer connection to the host.</summary>
		Connecting = 2,
		/// <summary>The guest successfully connected.</summary>
		Connected = 4,
		/// <summary>The guest disconnected.</summary>
		Disconnected = 8,
		/// <summary>The guest failed peer-to-peer negotiation.</summary>
		Failed = 16,
	}

	/// <summary>Host mode of operation.</summary>
	/// <remarks>Passed to ::ParsecHostStart.</remarks>
	public enum HostMode
	{
		/// <summary>The host intends to share their entire desktop. Permission and approval systems apply. Windows only.</summary>
		Desktop = 1,
		/// <summary>Parsec is integrated into a game. The game uses the `Submit` model to provide output.</summary>
		Game = 2,
	}

	/// <summary>Host event type.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	public enum HostEventType
	{
		/// <summary>A guest has changed connection state, `guestStateChange` is valid in ::ParsecHostEvent.</summary>
		GuestStateChange = 1,
		/// <summary>User-defined message from a guest, `userData is valid in ::ParsecHostEvent.</summary>
		UserData = 2,
		/// <summary>The host's Session ID has become invalid.</summary>
		InvalidSessionId = 4,
	}

	/// <summary>Client event type.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	public enum ClientEventType
	{
		/// <summary>
		///     A cursor mode change or image update is available, `cursor` is valid in ::ParsecClientEvent. Call
		///     ::ParsecGetBuffer in the case of an image update.
		/// </summary>
		Cursor = 1,
		/// <summary>Gamepad rumble event, `rumble` is valid in ::ParsecClientEvent.</summary>
		Rumble = 2,
		/// <summary>User-defined message from the host, `userData` is valid in ::ParsecClientEvent.</summary>
		UserData = 3,
		/// <summary>The client has been temporarily blocked from sending input and receiving host output.</summary>
		Blocked = 4,
		/// <summary>The client has returned to normal operation after receiving a ::CLIENT_EVENT_BLOCKED.</summary>
		Unblocked = 5,
	}

	/// <summary>Status codes indicating success, warning, or error.</summary>
	/// <remarks>
	///     <para>Returned by most Parsec SDK functions. ::PARSEC_OK is `0`,</para>
	///     <para>warnings are positive, errors are negative.</para>
	/// </remarks>
	/// <summary>Log level.</summary>
	/// <remarks>Passed through ::ParsecLogCallback set with ::ParsecSetLogCallback.</remarks>
	/// <summary>Keyboard input.</summary>
	/// <remarks>Member of ::ParsecKeyboardMessage.</remarks>
	/// <summary>Stateful modifier keys applied to keyboard input.</summary>
	/// <remarks>Member of ::ParsecKeyboardMessage. These values may be bitwise OR'd together.</remarks>
	/// <summary>Mouse button.</summary>
	/// <remarks>Member of ::ParsecMouseButtonMessage.</remarks>
	/// <summary>Gamepad button.</summary>
	/// <remarks>Member of ::ParsecGamepadButtonMessage.</remarks>
	/// <summary>Gamepad axes related to thumbsticks and triggers.</summary>
	/// <remarks>Member of ::ParsecGamepadAxisMessage.</remarks>
	/// <summary>Input message type.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Color formats for raw image data.</summary>
	/// <remarks>Member of ::ParsecFrame.</remarks>
	/// <summary>Network protocol used for peer-to-peer connections.</summary>
	/// <remarks>Member of ::ParsecClientConfig.</remarks>
	/// <summary>Video stream container.</summary>
	/// <remarks>Member of ::ParsecClientConfig.</remarks>
	/// <summary>PCM audio format.</summary>
	/// <remarks>Passed to ::ParsecHostSubmitAudio.</remarks>
	/// <summary>Guest connection lifecycle states.</summary>
	/// <remarks>Member of ::ParsecGuest and passed to ::ParsecHostGetGuests.</remarks>
	/// <summary>Host mode of operation.</summary>
	/// <remarks>Passed to ::ParsecHostStart.</remarks>
	/// <summary>Host event type.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	/// <summary>Client event type.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	/// <summary>::Parsec instance configuration.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecInit and returned by ::ParsecGetConfig. `clientPort` and `hostPort`</para>
	///     <para>serve as the first port used when the `bind` call is made internally. If the port is already in use,</para>
	///     <para>the next port will be tried until an open port has been found or 50 attempts have been made.</para>
	/// </remarks>
	/// <summary>Video frame properties.</summary>
	/// <remarks>Passed through ::ParsecFrameCallback after calling ::ParsecClientPollFrame.</remarks>
	/// <summary>Cursor properties.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecClientCursorEvent, which is itself a member of ::ParsecClientEvent,</para>
	///     <para>returned by ::ParsecClientPollEvents. Also passed to ::ParsecHostSubmitCursor to update the cursor while</para>
	///     <para>in ::HOST_GAME. When polled from ::ParsecClientPollEvents, `positionX` and `positionY` are</para>
	///     <para>affected by the values set via ::ParsecClientSetDimensions.</para>
	/// </remarks>
	/// <summary>Guest input permissions.</summary>
	/// <remarks>Member of ::ParsecGuest and passed to ::ParsecHostSetPermissions. Only relevant in ::HOST_DESKTOP.</remarks>
	/// <summary>Latency performance metrics.</summary>
	/// <remarks>Member of ::ParsecGuest and ::ParsecClientStatus.</remarks>
	/// <summary>Guest properties.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecGuestStateChangeEvent and ::ParsecUserDataEvent. Returned by ::ParsecHostGetGuests</para>
	///     <para>and ::ParsecHostPollInput.</para>
	/// </remarks>
	/// <summary>Keyboard message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Mouse button message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Mouse wheel message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Mouse motion message.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecMessage. Mouse motion can be sent in either relative or absolute mode via</para>
	///     <para>the `relative` member. Absolute mode treats the `x` and `y` values as the exact destination for where</para>
	///     <para>the cursor will appear. These values are sent from the client in device screen coordinates and are translated</para>
	///     <para>in accordance with the values set via ::ParsecClientSetDimensions. Relative mode `x` and `y` values are not</para>
	///     <para>
	///         affected by ::ParsecClientSetDimensions and move the cursor with a signed delta value from its previous
	///         location.
	///     </para>
	/// </remarks>
	/// <summary>Gamepad button message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Gamepad axis message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Gamepad unplug message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	/// <summary>Generic input message that can represent any message type.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecClientSendMessage or returned by ::ParsecHostPollInput while</para>
	///     <para>in ::HOST_GAME. The application can switch on the `type` member to see which `Message`</para>
	///     <para>struct should be evaluated. The `Message` structs are unioned.</para>
	/// </remarks>
	/// <summary>Client configuration.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecClientConnect. Regarding `resolutionX`, `resolutionY`, and `refreshRate`:</para>
	///     <para>These settings apply only in ::HOST_DESKTOP if the client is the first client to connect, and that client is</para>
	///     <para>
	///         the owner of the computer. Setting `resolutionX` or `resolutionY` to `0` will leave the host resolution
	///         unaffected,
	///     </para>
	///     <para>otherwise the host will attempt to find the closest matching resolution / refresh rate.</para>
	/// </remarks>
	/// <summary>Client connection health and status information.</summary>
	/// <remarks>Returned by ::ParsecClientGetStatus.</remarks>
	/// <summary>Cursor mode/image update event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	/// <summary>Gamepad rumble data event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	/// <summary>User-defined host message event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	/// <summary>Generic client event that can represent any event type.</summary>
	/// <remarks>
	///     <para>Returned by ::ParsecClientPollEvents. The application can switch on the `type` member to see</para>
	///     <para>which `Event` struct should be evaluated. The `Event` structs are unioned.</para>
	/// </remarks>
	/// <summary>Host configuration.</summary>
	/// <remarks>Member of ::ParsecHostStatus, passed to ::ParsecHostStart and ::ParsecHostSetConfig.</remarks>
	/// <summary>Host runtime status.</summary>
	/// <remarks>Returned by ::ParsecHostGetStatus.</remarks>
	/// <summary>Guest connection state change event.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	/// <summary>User-defined guest message event.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	/// <summary>Generic host event that can represent any event type.</summary>
	/// <remarks>
	///     <para>Returned by ::ParsecHostPollEvents. The application can switch on the `type` member</para>
	///     <para>to see which `Event` struct should be evaluated. The `Event` structs are unioned.</para>
	/// </remarks>
	/// <summary>OpenGL/GLES 32-bit unsigned integer.</summary>
	/// <remarks>Passed to ::ParsecHostGLSubmitFrame. Prevents obligatory include of GL headers.</remarks>
	/// <summary>D3D11 `ID3D11Device`.</summary>
	/// <remarks>Passed to ::ParsecHostD3D11SubmitFrame. Prevents obligatory include of d3d11.h.</remarks>
	/// <summary>D3D11 `ID3D11DeviceContext`.</summary>
	/// <remarks>Passed to ::ParsecHostD3D11SubmitFrame. Prevents obligatory include of d3d11.h.</remarks>
	/// <summary>D3D11 `ID3D11Texture2D`.</summary>
	/// <remarks>Passed to ::ParsecHostD3D11SubmitFrame. Prevents obligatory include of d3d11.h.</remarks>
	/// <summary>Fired when a new log message is available from the Parsec SDK.</summary>
	/// <param name="level">::ParsecLogLevel level value.</param>
	/// <param name="msg">Null-terminated UTF-8 string containing the full log message.</param>
	/// <param name="opaque">User supplied context passed to ::ParsecSetLogCallback.</param>
	/// <remarks>Passed to ::ParsecSetLogCallback.</remarks>
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void LogCallback(LogLevel level, sbyte* msg, IntPtr opaque);

	/// <summary>Fired synchronously if a new frame is available from the host.</summary>
	/// <param name="frame">Video frame properties.</param>
	/// <param name="image">The video frame buffer containing image data.</param>
	/// <param name="opaque">User supplied context passed to ::ParsecClientPollFrame.</param>
	/// <remarks>Passed to ::ParsecClientPollFrame.</remarks>
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void FrameCallback(IntPtr frame, byte* image, IntPtr opaque);

	/// <summary>Fired synchronously if new audio is available from the host.</summary>
	/// <param name="pcm">16-bit signed, two channel, 48KHz PCM audio samples.</param>
	/// <param name="frames">Number of audio frames.</param>
	/// <param name="opaque">User supplied context passed to ::ParsecClientPollAudio.</param>
	/// <remarks>Passed to ::ParsecClientPollAudio.</remarks>
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void AudioCallback(short* pcm, uint frames, IntPtr opaque);

	/// <summary>::Parsec instance configuration.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecInit and returned by ::ParsecGetConfig. `clientPort` and `hostPort`</para>
	///     <para>serve as the first port used when the `bind` call is made internally. If the port is already in use,</para>
	///     <para>the next port will be tried until an open port has been found or 50 attempts have been made.</para>
	/// </remarks>
	public unsafe struct ParsecConfig
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal int upnp;
			internal int clientPort;
			internal int hostPort;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ParsecConfig CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ParsecConfig(native.ToPointer(), skipVTables);
		}

		internal static ParsecConfig CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ParsecConfig(native, skipVTables);
		}

		private ParsecConfig(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ParsecConfig(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>`1` enables and maintains UPnP to assist NAT traversal, `0` disables it.</summary>
		public int Upnp
		{
			get => instance.upnp;
			set => instance.upnp = value;
		}

		/// <summary>First port tried for client connections. A value of `0` uses a pseudo random default.</summary>
		public int ClientPort
		{
			get => instance.clientPort;
			set => instance.clientPort = value;
		}

		/// <summary>First port used to accept host connections. A value of `0` uses a pseudo random default.</summary>
		public int HostPort
		{
			get => instance.hostPort;
			set => instance.hostPort = value;
		}
	}

	public sealed unsafe class Parsec : IDisposable
	{

		new private readonly static ConcurrentDictionary<IntPtr, Parsec> NativeToManagedMap =
			new ConcurrentDictionary<IntPtr, Parsec>();

		private bool disposedValue;

		public Parsec(ParsecConfig parsecConfig)
		{
			var pParsec = IntPtr.Zero;
			var status = Init(Version, parsecConfig, IntPtr.Zero, new IntPtr(&pParsec));
			if (status != Status.ParsecOk)
			{
				throw new Exception($"Failed to initialize Parsec: {status}");
			}

			Instance = pParsec;
		}

		public IntPtr Instance { get; }

		public static uint Version => Internal.VersionInternal();

		public void Dispose()
		{
			if (disposedValue) return;
			Destroy();
			GC.SuppressFinalize(this);
			disposedValue = true;
		}

		internal static void RecordNativeToManagedMapping(IntPtr native, Parsec managed)
		{
			NativeToManagedMap[native] = managed;
		}

		internal static bool TryGetNativeToManagedMapping(IntPtr native, out Parsec managed)
		{

			return NativeToManagedMap.TryGetValue(native, out managed);
		}

		~Parsec()
		{
			Destroy();
		}

		public Status ClientConnect(ClientConfig cfg, sbyte* sessionID, sbyte* peerID)
		{
			var arg0 = cfg.Instance;
			var ret = Internal.ClientConnect(Instance, new IntPtr(&arg0), sessionID, peerID);
			return ret;
		}
		
		public Status ClientConnect(ClientConfig cfg, string sessionID, string peerID)
		{
			if (cfg.Protocol != Protocol.Bud || cfg.Protocol != Protocol.Sctp)
			{
				// Important!! This is a workaround for a bug in the Parsec SDK.
				// If unset, the application will crash when the client connects.
				throw new InvalidOperationException("Invalid protocol");
			}
			
			var pSessionID = (sbyte*)MarshalUtil.StringToHGlobal(sessionID).ToPointer();
			var pPeerID = (sbyte*)MarshalUtil.StringToHGlobal(peerID).ToPointer();
			var status = ClientConnect(cfg, pSessionID, pPeerID);
			Marshal.FreeHGlobal(new IntPtr(pSessionID));
			Marshal.FreeHGlobal(new IntPtr(pPeerID));
			return status;
		}

		private void Destroy()
		{
			Internal.Destroy(Instance);
		}

		public void GetConfig(out ParsecConfig cfg)
		{
			var arg0 = IntPtr.Zero;
			Internal.GetConfig(Instance, new IntPtr(&arg0));
			cfg = ParsecConfig.CreateInstance(arg0);
		}

		public IntPtr GetBuffer(uint key)
		{
			var ret = Internal.GetBuffer(Instance, key);
			return ret;
		}

		public void ClientDisconnect()
		{
			Internal.ClientDisconnect(Instance);
		}

		public Status ClientGetStatus(ClientStatus status)
		{
			var instance = status.Instance;
			var arg0 = new IntPtr(&instance);
			var ret = Internal.ClientGetStatus(Instance, arg0);
			return ret;
		}

		public Status ClientSetDimensions(uint width, uint height, float scale)
		{
			var ret = Internal.ClientSetDimensions(Instance, width, height, scale);
			return ret;
		}

		public Status ClientPollFrame(FrameCallback callback, uint timeout, IntPtr opaque)
		{
			var arg0 = Marshal.GetFunctionPointerForDelegate(callback);
			var ret = Internal.ClientPollFrame(Instance, arg0, timeout, opaque);
			return ret;
		}

		public Status ClientPollAudio(AudioCallback callback, uint timeout, IntPtr opaque)
		{
			var arg0 = Marshal.GetFunctionPointerForDelegate(callback);
			var ret = Internal.ClientPollAudio(Instance, arg0, timeout, opaque);
			return ret;
		}

		public bool ClientPollEvents(uint timeout, out ClientEvent @event)
		{
			var arg1 = new IntPtr();
			var ret = Internal.ClientPollEvents(Instance, timeout, new IntPtr(&arg1));
			@event = ClientEvent.CreateInstance(arg1);
			return ret;
		}

		public Status ClientGLRenderFrame(uint timeout)
		{
			var ret = Internal.ClientGLRenderFrame(Instance, timeout);
			return ret;
		}

		public void ClientGLDestroy()
		{
			Internal.ClientGLDestroy(Instance);
		}

		public Status ClientSendMessage(Message msg)
		{
			var instance = msg.Instance;
			var arg0 = new IntPtr(&instance);
			var ret = Internal.ClientSendMessage(Instance, arg0);
			return ret;
		}

		public Status ClientSendUserData(uint id, sbyte* text)
		{
			var ret = Internal.ClientSendUserData(Instance, id, text);
			return ret;
		}
		
		public Status ClientSendUserData(uint id, string text)
		{
			var pText = (sbyte*)MarshalUtil.StringToHGlobal(text).ToPointer();
			var status = ClientSendUserData(id, pText);
			Marshal.FreeHGlobal(new IntPtr(pText));
			return status;
		}

		public Status HostStart(HostMode mode, HostConfig cfg, sbyte* sessionID)
		{
			var arg1 = cfg.Instance;
			var ret = Internal.HostStart(Instance, mode, new IntPtr(&arg1), sessionID);
			return ret;
		}
		
		public Status HostStart(HostMode mode, HostConfig cfg, string sessionID)
		{
			var pSessionID = (sbyte*)MarshalUtil.StringToHGlobal(sessionID).ToPointer();
			var status = HostStart(mode, cfg, pSessionID);
			Marshal.FreeHGlobal(new IntPtr(pSessionID));
			return status;
		}

		public void HostStop()
		{
			Internal.HostStop(Instance);
		}

		public void HostGetStatus(HostStatus status)
		{
			var instance = status.Instance;
			var arg0 = new IntPtr(&instance);
			Internal.HostGetStatus(Instance, arg0);
		}

		public Status HostSetConfig(HostConfig cfg, sbyte* sessionID)
		{
			var arg0 = cfg.Instance;
			var ret = Internal.HostSetConfig(Instance, new IntPtr(&arg0), sessionID);
			return ret;
		}
		
		public Status HostSetConfig(HostConfig cfg, string sessionID)
		{
			var pSessionID = (sbyte*)MarshalUtil.StringToHGlobal(sessionID).ToPointer();
			var status = HostSetConfig(cfg, pSessionID);
			Marshal.FreeHGlobal(new IntPtr(pSessionID));
			return status;
		}

		public uint HostGetGuests(GuestState state, IntPtr guests)
		{
			var ret = Internal.HostGetGuests(Instance, state, guests);
			return ret;
		}

		public Status HostKickGuest(uint guestID)
		{
			var ret = Internal.HostKickGuest(Instance, guestID);
			return ret;
		}

		public Status HostSendUserData(uint guestID, uint id, sbyte* text)
		{
			var ret = Internal.HostSendUserData(Instance, guestID, id, text);
			return ret;
		}
		
		public Status HostSendUserData(uint guestID, uint id, string text)
		{
			var pText = (sbyte*)MarshalUtil.StringToHGlobal(text).ToPointer();
			var status = HostSendUserData(guestID, id, pText);
			Marshal.FreeHGlobal(new IntPtr(pText));
			return status;
		}

		public bool HostPollEvents(uint timeout, out HostEvent @event)
		{
			var arg1 = new IntPtr();
			var ret = Internal.HostPollEvents(Instance, timeout, new IntPtr(&arg1));
			@event = HostEvent.CreateInstance(arg1);
			return ret;
		}

		public Status HostAllowGuest(sbyte* attemptID, bool allow)
		{
			var ret = Internal.HostAllowGuest(Instance, attemptID, allow);
			return ret;
		}
		
		public Status HostAllowGuest(string attemptID, bool allow)
		{
			var pAttemptID = (sbyte*)MarshalUtil.StringToHGlobal(attemptID).ToPointer();
			var status = HostAllowGuest(pAttemptID, allow);
			Marshal.FreeHGlobal(new IntPtr(pAttemptID));
			return status;
		}

		public Status HostSetPermissions(uint guestID, Permissions perms)
		{
			var instance = perms.Instance;
			var arg1 = new IntPtr(&instance);
			var ret = Internal.HostSetPermissions(Instance, guestID, arg1);
			return ret;
		}

		public bool HostPollInput(uint timeout, Guest guest, Message msg)
		{
			var instance = guest.Instance;
			var arg1 = new IntPtr(&instance);
			var instance2 = msg.Instance;
			var arg2 = new IntPtr(&instance2);
			var ret = Internal.HostPollInput(Instance, timeout, arg1, arg2);
			return ret;
		}

		public Status HostSubmitAudio(PcmFormat format, uint sampleRate, byte* pcm, uint frames)
		{
			var ret = Internal.HostSubmitAudio(Instance, format, sampleRate, pcm, frames);
			return ret;
		}

		public Status HostSubmitCursor(Cursor cursor, byte* image)
		{
			var instance = cursor.Instance;
			var arg0 = new IntPtr(&instance);
			var ret = Internal.HostSubmitCursor(Instance, arg0, image);
			return ret;
		}

		public Status HostSubmitRumble(uint guestID, uint gamepadID, byte motorBig, byte motorSmall)
		{
			var ret = Internal.HostSubmitRumble(Instance, guestID, gamepadID, motorBig, motorSmall);
			return ret;
		}

		public Status HostGLSubmitFrame(uint frame)
		{
			var ret = Internal.HostGLSubmitFrame(Instance, frame);
			return ret;
		}

		public Status HostD3D11SubmitFrame(IntPtr device, IntPtr context, IntPtr frame)
		{
			var ret = Internal.HostD3D11SubmitFrame(Instance, device, context, frame);
			return ret;
		}

		private static Status Init(uint ver, ParsecConfig cfg, IntPtr reserved, IntPtr ps)
		{
			var arg1 = cfg.Instance;
			var ret = Internal.Init(ver, new IntPtr(&arg1), reserved, ps);
			return ret;
		}

		public static void Free(IntPtr ptr)
		{
			Internal.FreeInternal(ptr);
		}

		public static void SetLogCallback(LogCallback callback, IntPtr opaque)
		{
			var arg0 = Marshal.GetFunctionPointerForDelegate(callback);
			Internal.SetLogCallback(arg0, opaque);
		}

		private struct Internal
		{
			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecDestroy", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void Destroy(IntPtr instance);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecGetConfig", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void GetConfig(IntPtr instance, IntPtr cfg);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecGetBuffer", CallingConvention = CallingConvention.Cdecl)]
			internal extern static IntPtr GetBuffer(IntPtr instance, uint key);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientConnect", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientConnect(IntPtr instance, IntPtr cfg, sbyte* sessionID, sbyte* peerID);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientDisconnect", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void ClientDisconnect(IntPtr instance);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientGetStatus", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientGetStatus(IntPtr instance, IntPtr status);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientSetDimensions", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientSetDimensions(IntPtr instance, uint width, uint height, float scale);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientPollFrame", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientPollFrame(IntPtr instance, IntPtr callback, uint timeout, IntPtr opaque);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientPollAudio", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientPollAudio(IntPtr instance, IntPtr callback, uint timeout, IntPtr opaque);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientPollEvents", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			internal extern static bool ClientPollEvents(IntPtr instance, uint timeout, IntPtr @event);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientGLRenderFrame", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientGLRenderFrame(IntPtr instance, uint timeout);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientGLDestroy", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void ClientGLDestroy(IntPtr instance);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientSendMessage", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientSendMessage(IntPtr instance, IntPtr msg);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecClientSendUserData", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status ClientSendUserData(IntPtr instance, uint id, sbyte* text);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostStart", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostStart(IntPtr instance, HostMode mode, IntPtr cfg, sbyte* sessionID);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostStop", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void HostStop(IntPtr instance);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostGetStatus", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void HostGetStatus(IntPtr instance, IntPtr status);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSetConfig", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSetConfig(IntPtr instance, IntPtr cfg, sbyte* sessionID);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostGetGuests", CallingConvention = CallingConvention.Cdecl)]
			internal extern static uint HostGetGuests(IntPtr instance, GuestState state, IntPtr guests);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostKickGuest", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostKickGuest(IntPtr instance, uint guestID);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSendUserData", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSendUserData(IntPtr instance, uint guestID, uint id, sbyte* text);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostPollEvents", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			internal extern static bool HostPollEvents(IntPtr instance, uint timeout, IntPtr @event);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostAllowGuest", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostAllowGuest(IntPtr instance, sbyte* attemptID, bool allow);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSetPermissions", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSetPermissions(IntPtr instance, uint guestID, IntPtr perms);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostPollInput", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			internal extern static bool HostPollInput(IntPtr instance, uint timeout, IntPtr guest, IntPtr msg);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSubmitAudio", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSubmitAudio(IntPtr instance, PcmFormat format, uint sampleRate, byte* pcm, uint frames);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSubmitCursor", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSubmitCursor(IntPtr instance, IntPtr cursor, byte* image);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostSubmitRumble", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostSubmitRumble(IntPtr instance, uint guestID, uint gamepadID, byte motorBig, byte motorSmall);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostGLSubmitFrame", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostGLSubmitFrame(IntPtr instance, uint frame);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecHostD3D11SubmitFrame", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status HostD3D11SubmitFrame(IntPtr instance, IntPtr device, IntPtr context, IntPtr frame);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecInit", CallingConvention = CallingConvention.Cdecl)]
			internal extern static Status Init(uint ver, IntPtr cfg, IntPtr reserved, IntPtr ps);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecFree", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void FreeInternal(IntPtr ptr);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecSetLogCallback", CallingConvention = CallingConvention.Cdecl)]
			internal extern static void SetLogCallback(IntPtr callback, IntPtr opaque);

			[SuppressUnmanagedCodeSecurity]
			[DllImport("parsec", EntryPoint = "ParsecVersion", CallingConvention = CallingConvention.Cdecl)]
			internal extern static uint VersionInternal();
		}
	}

	/// <summary>Video frame properties.</summary>
	/// <remarks>Passed through ::ParsecFrameCallback after calling ::ParsecClientPollFrame.</remarks>
	public unsafe struct Frame
	{
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		internal struct Internal
		{
			internal ColorFormat format;
			internal uint size;
			internal uint width;
			internal uint height;
			internal uint fullWidth;
			internal uint fullHeight;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Frame CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Frame(native.ToPointer(), skipVTables);
		}

		internal static Frame CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Frame(native, skipVTables);
		}

		private Frame(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Frame(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Color format.</summary>
		public ColorFormat Format
		{
			get => instance.format;
			set => instance.format = value;
		}

		/// <summary>Size in bytes of the `image` buffer parameter of ::ParsecFrameCallback.</summary>
		public uint Size
		{
			get => instance.size;
			set => instance.size = value;
		}

		/// <summary>Width in pixels of the visible area of the frame.</summary>
		public uint Width
		{
			get => instance.width;
			set => instance.width = value;
		}

		/// <summary>Height in pixels of the visible area of the frame.</summary>
		public uint Height
		{
			get => instance.height;
			set => instance.height = value;
		}

		/// <summary>Actual width of the frame including padding.</summary>
		public uint FullWidth
		{
			get => instance.fullWidth;
			set => instance.fullWidth = value;
		}

		/// <summary>Actual height of the frame including padding.</summary>
		public uint FullHeight
		{
			get => instance.fullHeight;
			set => instance.fullHeight = value;
		}
	}

	/// <summary>Cursor properties.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecClientCursorEvent, which is itself a member of ::ParsecClientEvent,</para>
	///     <para>returned by ::ParsecClientPollEvents. Also passed to ::ParsecHostSubmitCursor to update the cursor while</para>
	///     <para>in ::HOST_GAME. When polled from ::ParsecClientPollEvents, `positionX` and `positionY` are</para>
	///     <para>affected by the values set via ::ParsecClientSetDimensions.</para>
	/// </remarks>
	public unsafe struct Cursor
	{
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		internal struct Internal
		{
			internal uint size;
			internal uint positionX;
			internal uint positionY;
			internal ushort width;
			internal ushort height;
			internal ushort hotX;
			internal ushort hotY;
			internal byte modeUpdate;
			internal byte imageUpdate;
			internal byte relative;
			internal fixed byte pad[1];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Cursor CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Cursor(native.ToPointer(), skipVTables);
		}

		internal static Cursor CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Cursor(native, skipVTables);
		}

		private Cursor(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Cursor(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Size in bytes of the cursor image buffer.</summary>
		public uint Size
		{
			get => instance.size;
			set => instance.size = value;
		}

		/// <summary>When leaving relative mode, the horizontal position in screen coordinates where the cursor reappears.</summary>
		public uint PositionX
		{
			get => instance.positionX;
			set => instance.positionX = value;
		}

		/// <summary>When leaving relative mode, the vertical position in screen coordinates where the cursor reappears.</summary>
		public uint PositionY
		{
			get => instance.positionY;
			set => instance.positionY = value;
		}

		/// <summary>Width of the cursor image in pixels.</summary>
		public ushort Width
		{
			get => instance.width;
			set => instance.width = value;
		}

		/// <summary>Height of the cursor position in pixels.</summary>
		public ushort Height
		{
			get => instance.height;
			set => instance.height = value;
		}

		/// <summary>Horizontal pixel position of the cursor hotspot within the image.</summary>
		public ushort HotX
		{
			get => instance.hotX;
			set => instance.hotX = value;
		}

		/// <summary>Vertical pixel position of the cursor hotspot within the image.</summary>
		public ushort HotY
		{
			get => instance.hotY;
			set => instance.hotY = value;
		}

		/// <summary>`true` if the cursor mode should be updated. The `relative`, `positionX`, and `positionY` members are valid.</summary>
		public bool ModeUpdate
		{
			get => instance.modeUpdate != 0;
			set => instance.modeUpdate = (byte)(value ? 1 : 0);
		}

		/// <summary>
		///     `true` if the cursor image should be updated. The `width`, `height`, `hotX`, `hotY`, and `size` members are
		///     valid.
		/// </summary>
		public bool ImageUpdate
		{
			get => instance.imageUpdate != 0;
			set => instance.imageUpdate = (byte)(value ? 1 : 0);
		}

		/// <summary>
		///     `true` if in relative mode, meaning the client should submit mouse motion in relative distances rather than
		///     absolute screen coordinates.
		/// </summary>
		public bool Relative
		{
			get => instance.relative != 0;
			set => instance.relative = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 1);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 1; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Guest input permissions.</summary>
	/// <remarks>Member of ::ParsecGuest and passed to ::ParsecHostSetPermissions. Only relevant in ::HOST_DESKTOP.</remarks>
	public unsafe struct Permissions
	{
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		internal struct Internal
		{
			internal byte gamepad;
			internal byte keyboard;
			internal byte mouse;
			internal fixed byte pad[1];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Permissions CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Permissions(native.ToPointer(), skipVTables);
		}

		internal static Permissions CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Permissions(native, skipVTables);
		}

		private Permissions(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Permissions(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>The guest can send gamepad input.</summary>
		public bool Gamepad
		{
			get => instance.gamepad != 0;
			set => instance.gamepad = (byte)(value ? 1 : 0);
		}

		/// <summary>The guest can send keyboard input.</summary>
		public bool Keyboard
		{
			get => instance.keyboard != 0;
			set => instance.keyboard = (byte)(value ? 1 : 0);
		}

		/// <summary>The guest can send mouse button.</summary>
		public bool Mouse
		{
			get => instance.mouse != 0;
			set => instance.mouse = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 1);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 1; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Latency performance metrics.</summary>
	/// <remarks>Member of ::ParsecGuest and ::ParsecClientStatus.</remarks>
	public unsafe struct Metrics
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal float encodeLatency;
			internal float decodeLatency;
			internal float networkLatency;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Metrics CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Metrics(native.ToPointer(), skipVTables);
		}

		internal static Metrics CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Metrics(native, skipVTables);
		}

		private Metrics(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Metrics(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Average time in milliseconds for the host to encode a frame.</summary>
		public float EncodeLatency
		{
			get => instance.encodeLatency;
			set => instance.encodeLatency = value;
		}

		/// <summary>Average time in milliseconds for the client to decode a frame.</summary>
		public float DecodeLatency
		{
			get => instance.decodeLatency;
			set => instance.decodeLatency = value;
		}

		/// <summary>Average round trip time between the client and host.</summary>
		public float NetworkLatency
		{
			get => instance.networkLatency;
			set => instance.networkLatency = value;
		}
	}

	/// <summary>Guest properties.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecGuestStateChangeEvent and ::ParsecUserDataEvent. Returned by ::ParsecHostGetGuests</para>
	///     <para>and ::ParsecHostPollInput.</para>
	/// </remarks>
	public unsafe struct Guest
	{
		[StructLayout(LayoutKind.Sequential, Size = 408)]
		internal struct Internal
		{
			internal Permissions.Internal perms;
			internal Metrics.Internal metrics;
			internal GuestState state;
			internal uint id;
			internal uint userID;
			internal fixed sbyte name[320];
			internal fixed sbyte attemptID[56];
			internal byte owner;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Guest CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Guest(native.ToPointer(), skipVTables);
		}

		internal static Guest CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Guest(native, skipVTables);
		}

		private Guest(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Guest(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Guest input permissions. ::HOST_DESKTOP only.</summary>
		public Permissions Perms;

		/// <summary>Latency performance metrics, only valid in state ::GUEST_CONNECTED.</summary>
		public Metrics Metrics;

		/// <summary>Guest connection lifecycle states.</summary>
		public GuestState State
		{
			get => instance.state;
			set => instance.state = value;
		}

		/// <summary>Guest ID passed to various host functions.</summary>
		public uint Id
		{
			get => instance.id;
			set => instance.id = value;
		}

		/// <summary>Parsec unique user ID.</summary>
		public uint UserID
		{
			get => instance.userID;
			set => instance.userID = value;
		}

		/// <summary>UTF-8 null-terminated name guest name string.</summary>
		public string Name
		{
			get
			{
				fixed (sbyte* arrPtr = instance.name)
				{
					return MarshalUtil.GetString(arrPtr, 320);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.name)
				{
					MarshalUtil.SetString(arrPtr, 320, value);
				}
			}
		}

		/// <summary>
		///     Unique connection ID valid while `state` is ::GUEST_WAITING, otherwise filled with zeroes. ::HOST_DESKTOP
		///     only.
		/// </summary>
		public string AttemptID
		{
			get
			{
				fixed (sbyte* arrPtr = instance.attemptID)
				{
					return MarshalUtil.GetString(arrPtr, 56);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.attemptID)
				{
					MarshalUtil.SetString(arrPtr, 56, value);
				}
			}
		}

		/// <summary>The guest is also the owner of the host computer. ::HOST_DESKTOP only.</summary>
		public bool Owner
		{
			get => instance.owner != 0;
			set => instance.owner = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Keyboard message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct KeyboardMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal Keycode code;
			internal KeyModifier mod;
			internal byte pressed;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static KeyboardMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new KeyboardMessage(native.ToPointer(), skipVTables);
		}

		internal static KeyboardMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new KeyboardMessage(native, skipVTables);
		}

		private KeyboardMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private KeyboardMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Keyboard input.</summary>
		public Keycode Code
		{
			get => instance.code;
			set => instance.code = value;
		}

		/// <summary>Stateful modifier keys applied to keyboard input.</summary>
		public KeyModifier Mod
		{
			get => instance.mod;
			set => instance.mod = value;
		}

		/// <summary>`true` if pressed, `false` if released.</summary>
		public bool Pressed
		{
			get => instance.pressed != 0;
			set => instance.pressed = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Mouse button message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct MouseButtonMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		internal struct Internal
		{
			internal MouseButton button;
			internal byte pressed;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static MouseButtonMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new MouseButtonMessage(native.ToPointer(), skipVTables);
		}

		internal static MouseButtonMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new MouseButtonMessage(native, skipVTables);
		}

		private MouseButtonMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private MouseButtonMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Mouse button.</summary>
		public MouseButton Button
		{
			get => instance.button;
			set => instance.button = value;
		}

		/// <summary>`true` if clicked, `false` if released.</summary>
		public bool Pressed
		{
			get => instance.pressed != 0;
			set => instance.pressed = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Mouse wheel message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct MouseWheelMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		internal struct Internal
		{
			internal int x;
			internal int y;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static MouseWheelMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new MouseWheelMessage(native.ToPointer(), skipVTables);
		}

		internal static MouseWheelMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new MouseWheelMessage(native, skipVTables);
		}

		private MouseWheelMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private MouseWheelMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Horizontal delta of mouse wheel rotation. Negative values scroll left.</summary>
		public int X
		{
			get => instance.x;
			set => instance.x = value;
		}

		/// <summary>Vertical delta of mouse wheel rotation. Negative values scroll up.</summary>
		public int Y
		{
			get => instance.y;
			set => instance.y = value;
		}
	}

	/// <summary>Mouse motion message.</summary>
	/// <remarks>
	///     <para>Member of ::ParsecMessage. Mouse motion can be sent in either relative or absolute mode via</para>
	///     <para>the `relative` member. Absolute mode treats the `x` and `y` values as the exact destination for where</para>
	///     <para>the cursor will appear. These values are sent from the client in device screen coordinates and are translated</para>
	///     <para>in accordance with the values set via ::ParsecClientSetDimensions. Relative mode `x` and `y` values are not</para>
	///     <para>
	///         affected by ::ParsecClientSetDimensions and move the cursor with a signed delta value from its previous
	///         location.
	///     </para>
	/// </remarks>
	public unsafe struct MouseMotionMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal int x;
			internal int y;
			internal byte relative;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static MouseMotionMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new MouseMotionMessage(native.ToPointer(), skipVTables);
		}

		internal static MouseMotionMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new MouseMotionMessage(native, skipVTables);
		}

		private MouseMotionMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private MouseMotionMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>
		///     The absolute horizontal screen coordinate of the cursor  if `relative` is `false`, or the delta (can be
		///     negative) if `relative` is `true`.
		/// </summary>
		public int X
		{
			get => instance.x;
			set => instance.x = value;
		}

		/// <summary>
		///     The absolute vertical screen coordinate of the cursor if `relative` is `false`, or the delta (can be negative)
		///     if `relative` is `true`.
		/// </summary>
		public int Y
		{
			get => instance.y;
			set => instance.y = value;
		}

		/// <summary>`true` for relative mode, `false` for absolute mode. See above.</summary>
		public bool Relative
		{
			get => instance.relative != 0;
			set => instance.relative = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Gamepad button message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct GamepadButtonMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal GamepadButton button;
			internal uint id;
			internal byte pressed;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static GamepadButtonMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new GamepadButtonMessage(native.ToPointer(), skipVTables);
		}

		internal static GamepadButtonMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new GamepadButtonMessage(native, skipVTables);
		}

		private GamepadButtonMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private GamepadButtonMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Gamepad button.</summary>
		public GamepadButton Button
		{
			get => instance.button;
			set => instance.button = value;
		}

		/// <summary>Unique client-provided index identifying the gamepad.</summary>
		public uint Id
		{
			get => instance.id;
			set => instance.id = value;
		}

		/// <summary>`true` if the button was pressed, `false` if released.</summary>
		public bool Pressed
		{
			get => instance.pressed != 0;
			set => instance.pressed = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Gamepad axis message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct GamepadAxisMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		internal struct Internal
		{
			internal GamepadAxis axis;
			internal uint id;
			internal short value;
			internal fixed byte pad[2];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static GamepadAxisMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new GamepadAxisMessage(native.ToPointer(), skipVTables);
		}

		internal static GamepadAxisMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new GamepadAxisMessage(native, skipVTables);
		}

		private GamepadAxisMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private GamepadAxisMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Gamepad axes related to thumbsticks and triggers.</summary>
		public GamepadAxis Axis
		{
			get => instance.axis;
			set => instance.axis = value;
		}

		/// <summary>Unique client-provided index identifying the gamepad.</summary>
		public uint Id
		{
			get => instance.id;
			set => instance.id = value;
		}

		/// <summary>The new value of the axis between -32,768 (left/down) and 32,767 (right/up).</summary>
		public short Value
		{
			get => instance.value;
			set => instance.value = value;
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 2);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 2; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Gamepad unplug message.</summary>
	/// <remarks>Member of ::ParsecMessage.</remarks>
	public unsafe struct GamepadUnplugMessage
	{
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		internal struct Internal
		{
			internal uint id;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static GamepadUnplugMessage CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new GamepadUnplugMessage(native.ToPointer(), skipVTables);
		}

		internal static GamepadUnplugMessage CreateInstance(Internal native, bool skipVTables = false)
		{
			return new GamepadUnplugMessage(native, skipVTables);
		}

		private GamepadUnplugMessage(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private GamepadUnplugMessage(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Unique client-provided index identifying the gamepad.</summary>
		public uint Id
		{
			get => instance.id;
			set => instance.id = value;
		}
	}

	/// <summary>Generic input message that can represent any message type.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecClientSendMessage or returned by ::ParsecHostPollInput while</para>
	///     <para>in ::HOST_GAME. The application can switch on the `type` member to see which `Message`</para>
	///     <para>struct should be evaluated. The `Message` structs are unioned.</para>
	/// </remarks>
	public unsafe struct Message
	{
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		internal struct Internal
		{
			[FieldOffset(0)] internal MessageType type;

			[FieldOffset(4)] internal KeyboardMessage.Internal keyboard;

			[FieldOffset(4)] internal MouseButtonMessage.Internal mouseButton;

			[FieldOffset(4)] internal MouseWheelMessage.Internal mouseWheel;

			[FieldOffset(4)] internal MouseMotionMessage.Internal mouseMotion;

			[FieldOffset(4)] internal GamepadButtonMessage.Internal gamepadButton;

			[FieldOffset(4)] internal GamepadAxisMessage.Internal gamepadAxis;

			[FieldOffset(4)] internal GamepadUnplugMessage.Internal gamepadUnplug;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static Message CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new Message(native.ToPointer(), skipVTables);
		}

		internal static Message CreateInstance(Internal native, bool skipVTables = false)
		{
			return new Message(native, skipVTables);
		}

		private Message(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private Message(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Input message type.</summary>
		public MessageType Type
		{
			get => instance.type;
			set => instance.type = value;
		}

		/// <summary>Keyboard message.</summary>
		public KeyboardMessage Keyboard;

		/// <summary>Mouse button message.</summary>
		public MouseButtonMessage MouseButton;

		/// <summary>Mouse wheel message.</summary>
		public MouseWheelMessage MouseWheel;

		/// <summary>Mouse motion message.</summary>
		public MouseMotionMessage MouseMotion;

		/// <summary>Gamepad button message.</summary>
		public GamepadButtonMessage GamepadButton;

		/// <summary>Gamepad axis message.</summary>
		public GamepadAxisMessage GamepadAxis;

		/// <summary>Gamepad unplug message.</summary>
		public GamepadUnplugMessage GamepadUnplug;
	}

	/// <summary>Client configuration.</summary>
	/// <remarks>
	///     <para>Passed to ::ParsecClientConnect. Regarding `resolutionX`, `resolutionY`, and `refreshRate`:</para>
	///     <para>These settings apply only in ::HOST_DESKTOP if the client is the first client to connect, and that client is</para>
	///     <para>
	///         the owner of the computer. Setting `resolutionX` or `resolutionY` to `0` will leave the host resolution
	///         unaffected,
	///     </para>
	///     <para>otherwise the host will attempt to find the closest matching resolution / refresh rate.</para>
	/// </remarks>
	public unsafe struct ClientConfig
	{
		[StructLayout(LayoutKind.Sequential, Size = 28)]
		internal struct Internal
		{
			internal int decoderSoftware;
			internal int mediaContainer;
			internal Protocol protocol;
			internal int resolutionX;
			internal int resolutionY;
			internal int refreshRate;
			internal byte pngCursor;
			internal fixed byte pad[3];
		}

		private Internal instance;

		internal Internal Instance => instance;

		internal static ClientConfig CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientConfig(native.ToPointer(), skipVTables);
		}

		internal static ClientConfig CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientConfig(native, skipVTables);
		}

		private ClientConfig(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientConfig(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>`true` to force decoding of video frames via a software implementation.</summary>
		public int DecoderSoftware
		{
			get => instance.decoderSoftware;
			set => instance.decoderSoftware = value;
		}

		/// <summary>::ParsecContainer value.</summary>
		public int MediaContainer
		{
			get => instance.mediaContainer;
			set => instance.mediaContainer = value;
		}

		/// <summary>::ParsecProtocol value.</summary>
		public Protocol Protocol
		{
			get => instance.protocol;
			set => instance.protocol = value;
		}

		/// <summary>See above.</summary>
		public int ResolutionX
		{
			get => instance.resolutionX;
			set => instance.resolutionX = value;
		}

		/// <summary>See above.</summary>
		public int ResolutionY
		{
			get => instance.resolutionY;
			set => instance.resolutionY = value;
		}

		/// <summary>See above.</summary>
		public int RefreshRate
		{
			get => instance.refreshRate;
			set => instance.refreshRate = value;
		}

		/// <summary>
		///     `true` to return compressed PNG cursor images during ::ParsecClientPollEvents, `false` to return a 32-bit RGBA
		///     image.
		/// </summary>
		public bool PngCursor
		{
			get => instance.pngCursor != 0;
			set => instance.pngCursor = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Client connection health and status information.</summary>
	/// <remarks>Returned by ::ParsecClientGetStatus.</remarks>
	public unsafe struct ClientStatus
	{
		[StructLayout(LayoutKind.Sequential, Size = 72)]
		internal struct Internal
		{
			internal Metrics.Internal metrics;
			internal fixed sbyte attemptID[56];
			internal byte networkFailure;
			internal byte decoderFallback;
			internal fixed byte pad[1];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ClientStatus CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientStatus(native.ToPointer(), skipVTables);
		}

		internal static ClientStatus CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientStatus(native, skipVTables);
		}

		private ClientStatus(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientStatus(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Latency performance metrics.</summary>
		public Metrics Metrics;

		/// <summary>Most recent connection `attemptID`. Valid even if ::ParsecClientConnect does not return ::PARSEC_OK.</summary>
		public string AttemptID
		{
			get
			{
				fixed (sbyte* arrPtr = instance.attemptID)
				{
					return MarshalUtil.GetString(arrPtr, 56);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.attemptID)
				{
					MarshalUtil.SetString(arrPtr, 56, value);
				}
			}
		}

		/// <summary>Client is currently experiencing network failure.</summary>
		public bool NetworkFailure
		{
			get => instance.networkFailure != 0;
			set => instance.networkFailure = (byte)(value ? 1 : 0);
		}

		/// <summary>
		///     `true` if the client had to fallback to software decoding after being unable to internally initialize a
		///     hardware accelerated decoder.
		/// </summary>
		public bool DecoderFallback
		{
			get => instance.decoderFallback != 0;
			set => instance.decoderFallback = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 1);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 1; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Cursor mode/image update event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	internal unsafe struct ClientCursorEventInternal
	{
		[StructLayout(LayoutKind.Sequential, Size = 28)]
		internal struct Internal
		{
			internal Cursor.Internal cursor;
			internal uint key;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ClientCursorEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientCursorEventInternal(native.ToPointer(), skipVTables);
		}

		internal static ClientCursorEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientCursorEventInternal(native, skipVTables);
		}

		private ClientCursorEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientCursorEventInternal(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}
	}

	/// <summary>Gamepad rumble data event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	internal unsafe struct ClientRumbleEventInternal
	{
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		internal struct Internal
		{
			internal uint gamepadID;
			internal byte motorBig;
			internal byte motorSmall;
			internal fixed byte pad[2];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ClientRumbleEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientRumbleEventInternal(native.ToPointer(), skipVTables);
		}

		internal static ClientRumbleEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientRumbleEventInternal(native, skipVTables);
		}

		private ClientRumbleEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientRumbleEventInternal(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}
	}

	/// <summary>User-defined host message event.</summary>
	/// <remarks>Member of ::ParsecClientEvent.</remarks>
	internal unsafe struct ClientUserDataEventInternal
	{
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		internal struct Internal
		{
			internal uint id;
			internal uint key;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ClientUserDataEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientUserDataEventInternal(native.ToPointer(), skipVTables);
		}

		internal static ClientUserDataEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientUserDataEventInternal(native, skipVTables);
		}

		private ClientUserDataEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientUserDataEventInternal(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}
	}

	/// <summary>Generic client event that can represent any event type.</summary>
	/// <remarks>
	///     <para>Returned by ::ParsecClientPollEvents. The application can switch on the `type` member to see</para>
	///     <para>which `Event` struct should be evaluated. The `Event` structs are unioned.</para>
	/// </remarks>
	internal unsafe struct ClientEventInternal
	{
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		internal struct Internal
		{
			[FieldOffset(0)] internal ClientEventType type;

			[FieldOffset(4)] internal ClientCursorEventInternal.Internal cursor;

			[FieldOffset(4)] internal ClientRumbleEventInternal.Internal rumble;

			[FieldOffset(4)] internal ClientUserDataEventInternal.Internal userData;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static ClientEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new ClientEventInternal(native.ToPointer(), skipVTables);
		}

		internal static ClientEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new ClientEventInternal(native, skipVTables);
		}

		private ClientEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private ClientEventInternal(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Client event type.</summary>
		public ClientEventType Type
		{
			get => instance.type;
			set => instance.type = value;
		}
	}

	public class ClientCursorEvent : ClientEvent
	{
		/// <summary>Cursor properties.</summary>
		public Cursor Cursor { get; internal set; }

		/// <summary>Buffer lookup key passed to ::ParsecGetBuffer to retrieve the cursor image, if available.</summary>
		public uint Key { get; internal set; }

		public override ClientEventType Type => ClientEventType.Cursor;
	}

	public class ClientRumbleEvent : ClientEvent
	{
		/// <summary>Unique client-assigned index identifying the gamepad connected to the client.</summary>
		public uint GamepadID { get; internal set; }

		/// <summary>8-bit unsigned value for large motor vibration.</summary>
		public byte MotorBig { get; internal set; }

		/// <summary>8-bit unsigned value for small motor vibration.</summary>
		public byte MotorSmall { get; internal set; }

		public byte[] Pad { get; internal set; }
		
		public override ClientEventType Type => ClientEventType.Rumble;
	}

	public class ClientUserDataEvent : ClientEvent
	{
		/// <summary>User-defined message ID set by the host.</summary>
		public uint Id { get; internal set; }

		/// <summary>Buffer lookup key passed to ::ParsecGetBuffer to retrieve the message.</summary>
		public uint Key { get; internal set; }
		
		public override ClientEventType Type => ClientEventType.UserData;
	}

	public class ClientBlockedEvent : ClientEvent
	{
		public override ClientEventType Type => ClientEventType.Blocked;
	}

	public class ClientUnblockedEvent : ClientEvent
	{
		public override ClientEventType Type => ClientEventType.Unblocked;
	}
	
	public abstract class ClientEvent
	{
		public abstract ClientEventType Type { get; }
		
		internal unsafe static ClientEvent CreateInstance(IntPtr native)
		{
			var internalEvent = ClientEventInternal.CreateInstance(native);
			switch (internalEvent.Type)
			{
				case ClientEventType.Cursor:
					return new ClientCursorEvent
					{
						Cursor = Cursor.CreateInstance(internalEvent.Instance.cursor.cursor),
						Key = internalEvent.Instance.cursor.key,
					};
				case ClientEventType.Rumble:
					var rumble = internalEvent.Instance.rumble;
					return new ClientRumbleEvent
					{
						GamepadID = rumble.gamepadID,
						MotorBig = rumble.motorBig,
						MotorSmall = rumble.motorSmall,
						Pad = MarshalUtil.GetArray<byte>(rumble.pad, 2),
					};
				case ClientEventType.UserData:
					var userData = internalEvent.Instance.userData;
					return new ClientUserDataEvent
					{
						Id = userData.id,
						Key = userData.key,
					};
				case ClientEventType.Blocked:
					return new ClientBlockedEvent();
				case ClientEventType.Unblocked:
					return new ClientUnblockedEvent();
				default:
					throw new ArgumentOutOfRangeException(nameof(internalEvent.Type));
			}
		}
	}

	/// <summary>Host configuration.</summary>
	/// <remarks>Member of ::ParsecHostStatus, passed to ::ParsecHostStart and ::ParsecHostSetConfig.</remarks>
	public unsafe struct HostConfig
	{
		[StructLayout(LayoutKind.Sequential, Size = 880)]
		public struct Internal
		{
			internal int resolutionX;
			internal int resolutionY;
			internal int refreshRate;
			internal int adminMute;
			internal int exclusiveInput;
			internal int encoderFPS;
			internal int encoderMaxBitrate;
			internal int encoderH265;
			internal int maxGuests;
			internal fixed sbyte name[256];
			internal fixed sbyte desc[512];
			internal fixed sbyte gameID[72];
			internal byte publicGame;
			internal fixed byte pad[3];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static HostConfig CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new HostConfig(native.ToPointer(), skipVTables);
		}

		internal static HostConfig CreateInstance(Internal native, bool skipVTables = false)
		{
			return new HostConfig(native, skipVTables);
		}

		private HostConfig(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private HostConfig(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Resolution width. ::HOST_DESKTOP owner only.</summary>
		public int ResolutionX
		{
			get => instance.resolutionX;
			set => instance.resolutionX = value;
		}

		/// <summary>Resolution height. ::HOST_DESKTOP owner only.</summary>
		public int ResolutionY
		{
			get => instance.resolutionY;
			set => instance.resolutionY = value;
		}

		/// <summary>Refresh rate in Hz. ::HOST_DESKTOP owner only.</summary>
		public int RefreshRate
		{
			get => instance.refreshRate;
			set => instance.refreshRate = value;
		}

		/// <summary>Mute local audio on owner connection. ::HOST_DESKTOP owner only.</summary>
		public int AdminMute
		{
			get => instance.adminMute;
			set => instance.adminMute = value;
		}

		/// <summary>Block remote input when local host input occurs. ::HOST_DESKTOP only.</summary>
		public int ExclusiveInput
		{
			get => instance.exclusiveInput;
			set => instance.exclusiveInput = value;
		}

		/// <summary>Desired frames per second.</summary>
		public int EncoderFPS
		{
			get => instance.encoderFPS;
			set => instance.encoderFPS = value;
		}

		/// <summary>Maximum output bitrate in Mbps, split between guests.</summary>
		public int EncoderMaxBitrate
		{
			get => instance.encoderMaxBitrate;
			set => instance.encoderMaxBitrate = value;
		}

		/// <summary>Allow H.265 codec.</summary>
		public int EncoderH265
		{
			get => instance.encoderH265;
			set => instance.encoderH265 = value;
		}

		/// <summary>Total number of guests allowed at once. This number should not include the local host.</summary>
		public int MaxGuests
		{
			get => instance.maxGuests;
			set => instance.maxGuests = value;
		}

		/// <summary>UTF-8 null-terminated name string. May be zeroed to use hostname.</summary>
		public string Name
		{
			get
			{
				fixed (sbyte* arrPtr = instance.name)
				{
					return MarshalUtil.GetString(arrPtr, 256);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.name)
				{
					MarshalUtil.SetString(arrPtr, 256, value);
				}
			}
		}

		/// <summary>UTF-8 null-terminated description string. ::HOST_GAME only.</summary>
		public string Desc
		{
			get
			{
				fixed (sbyte* arrPtr = instance.desc)
				{
					return MarshalUtil.GetString(arrPtr, 512);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.desc)
				{
					MarshalUtil.SetString(arrPtr, 512, value);
				}
			}
		}

		/// <summary>Game unique identifier issued by Parsec. ::HOST_GAME only.</summary>
		public string GameID
		{
			get
			{
				fixed (sbyte* arrPtr = instance.gameID)
				{
					return MarshalUtil.GetString(arrPtr, 72);
				}
			}
			set
			{
				fixed (sbyte* arrPtr = instance.gameID)
				{
					MarshalUtil.SetString(arrPtr, 72, value);
				}
			}
		}

		/// <summary>Set to `true` to allow the hosting session to be visible publicly in the Parsec Arcade. ::HOST_GAME only.</summary>
		public bool PublicGame
		{
			get => instance.publicGame != 0;
			set => instance.publicGame = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 3);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 3; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Host runtime status.</summary>
	/// <remarks>Returned by ::ParsecHostGetStatus.</remarks>
	public unsafe struct HostStatus
	{
		[StructLayout(LayoutKind.Sequential, Size = 888)]
		internal struct Internal
		{
			internal HostConfig.Internal cfg;
			internal uint numGuests;
			internal byte running;
			internal byte invalidSessionID;
			internal byte gamepadSupport;
			internal fixed byte pad[1];
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static HostStatus CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new HostStatus(native.ToPointer(), skipVTables);
		}

		internal static HostStatus CreateInstance(Internal native, bool skipVTables = false)
		{
			return new HostStatus(native, skipVTables);
		}

		private HostStatus(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private HostStatus(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>The currently active host configuration.</summary>
		public HostConfig Cfg
		{
			get => HostConfig.CreateInstance(instance.cfg);
			set => instance.cfg = value.Instance;
		}

		/// <summary>The number of guests currently in state ::GUEST_CONNECTED.</summary>
		public uint NumGuests
		{
			get => instance.numGuests;
			set => instance.numGuests = value;
		}

		/// <summary>The host is currently accepting guests after calling ::ParsecHostStart.</summary>
		public bool Running
		{
			get => instance.running != 0;
			set => instance.running = (byte)(value ? 1 : 0);
		}

		/// <summary>
		///     `true` if the host's Session ID has become invalid. The host must call ::ParsecHostSetConfig with a valid
		///     `sessionID` to continue hosting.
		/// </summary>
		public bool InvalidSessionID
		{
			get => instance.invalidSessionID != 0;
			set => instance.invalidSessionID = (byte)(value ? 1 : 0);
		}

		/// <summary>`true` if the virtual gamepad driver is working properly, otherwise `false`. ::HOST_DESKTOP only.</summary>
		public bool GamepadSupport
		{
			get => instance.gamepadSupport != 0;
			set => instance.gamepadSupport = (byte)(value ? 1 : 0);
		}

		public byte[] Pad
		{
			get
			{
				fixed (byte* arrPtr = instance.pad)
				{
					return MarshalUtil.GetArray<byte>(arrPtr, 1);
				}
			}
			set
			{
				if (value != null)
				{
					for (var i = 0; i < 1; i++)
					{
						instance.pad[i] = value[i];
					}
				}
			}
		}
	}

	/// <summary>Guest connection state change event.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	internal unsafe struct GuestStateChangeEventInternal
	{
		[StructLayout(LayoutKind.Sequential, Size = 408)]
		internal struct Internal
		{
			internal Guest.Internal guest;
		}

		internal Internal Instance { get; }

		internal static GuestStateChangeEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new GuestStateChangeEventInternal(native.ToPointer(), skipVTables);
		}

		internal static GuestStateChangeEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new GuestStateChangeEventInternal(native, skipVTables);
		}

		private GuestStateChangeEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			Instance = native;
		}

		private GuestStateChangeEventInternal(void* native, bool skipVTables = false) : this()
		{
			Instance = *(Internal*)native;
		}
	}

	/// <summary>User-defined guest message event.</summary>
	/// <remarks>Member of ::ParsecHostEvent.</remarks>
	internal unsafe struct UserDataEventInternal
	{
		[StructLayout(LayoutKind.Sequential, Size = 416)]
		internal struct Internal
		{
			internal Guest.Internal guest;
			internal uint id;
			internal uint key;
		}

		internal Internal Instance { get; }

		internal static UserDataEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new UserDataEventInternal(native.ToPointer(), skipVTables);
		}

		internal static UserDataEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new UserDataEventInternal(native, skipVTables);
		}

		private UserDataEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			Instance = native;
		}

		private UserDataEventInternal(void* native, bool skipVTables = false) : this()
		{
			Instance = *(Internal*)native;
		}
	}

	/// <summary>Generic host event that can represent any event type.</summary>
	/// <remarks>
	///     <para>Returned by ::ParsecHostPollEvents. The application can switch on the `type` member</para>
	///     <para>to see which `Event` struct should be evaluated. The `Event` structs are unioned.</para>
	/// </remarks>
	internal unsafe struct HostEventInternal
	{
		[StructLayout(LayoutKind.Explicit, Size = 420)]
		internal struct Internal
		{
			[FieldOffset(0)] internal HostEventType type;

			[FieldOffset(4)] internal GuestStateChangeEventInternal.Internal guestStateChange;

			[FieldOffset(4)] internal UserDataEventInternal.Internal userData;
		}

		private Internal instance;
		internal Internal Instance => instance;

		internal static HostEventInternal CreateInstance(IntPtr native, bool skipVTables = false)
		{
			return new HostEventInternal(native.ToPointer(), skipVTables);
		}

		internal static HostEventInternal CreateInstance(Internal native, bool skipVTables = false)
		{
			return new HostEventInternal(native, skipVTables);
		}

		private HostEventInternal(Internal native, bool skipVTables = false)
			: this()
		{
			instance = native;
		}

		private HostEventInternal(void* native, bool skipVTables = false) : this()
		{
			instance = *(Internal*)native;
		}

		/// <summary>Host event type.</summary>
		public HostEventType Type
		{
			get => instance.type;
			set => instance.type = value;
		}
	}

	public class GuestStateChangeEvent : HostEvent
	{
		/// <summary>Guest properties. The `state` member can be used to evaluate the guest's state change.</summary>
		public Guest Guest { get; internal set; }

		public override HostEventType Type => HostEventType.GuestStateChange;
	}

	public class UserDataEvent : HostEvent
	{
		/// <summary>Guest ::ParsecGuest properties.</summary>
		public Guest Guest { get; internal set; }

		/// <summary>User-defined message ID set by the client.</summary>
		public uint Id { get; internal set; }

		/// <summary>Buffer lookup key passed to ::ParsecGetBuffer to retrieve the message.</summary>
		public uint Key { get; internal set; }

		public override HostEventType Type => HostEventType.UserData;
	}

	public abstract class HostEvent
	{
		public abstract HostEventType Type { get; }

		internal static HostEvent CreateInstance(IntPtr native)
		{
			var internalEvent = HostEventInternal.CreateInstance(native);
			switch (internalEvent.Type)
			{
				case HostEventType.UserData:
					return new UserDataEvent
					{
						Guest = Guest.CreateInstance(internalEvent.Instance.userData.guest),
						Id = internalEvent.Instance.userData.id,
						Key = internalEvent.Instance.userData.key
					};
				case HostEventType.GuestStateChange:
					return new GuestStateChangeEvent
					{
						Guest = Guest.CreateInstance(internalEvent.Instance.guestStateChange.guest)
					};
				case HostEventType.InvalidSessionId:
					throw new InvalidOperationException("Invalid session ID");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}