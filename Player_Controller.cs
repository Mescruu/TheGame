<?xml version="1.0" encoding="utf-16"?>
<Project ToolsVersion="4.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <LangVersion>4</LangVersion>
  </PropertyGroup>
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>10.0.20506</ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <RootNamespace>
    </RootNamespace>
    <ProjectGuid>{8A1474A9-EA66-8089-685E-9BB9BFDBD164}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <AssemblyName>Assembly-CSharp</AssemblyName>
    <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <BaseDirectory>Assets</BaseDirectory>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>Temp\bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE;UNITY_5_3_OR_NEWER;UNITY_5_4_OR_NEWER;UNITY_5_5_OR_NEWER;UNITY_5_6_OR_NEWER;UNITY_2017_1_OR_NEWER;UNITY_2017_2_OR_NEWER;UNITY_2017_3_OR_NEWER;UNITY_2017_3_0;UNITY_2017_3;UNITY_2017;UNITY_ANALYTICS;ENABLE_AUDIO;ENABLE_CACHING;ENABLE_CLOTH;ENABLE_DUCK_TYPING;ENABLE_GENERICS;ENABLE_PVR_GI;ENABLE_MICROPHONE;ENABLE_MULTIPLE_DISPLAYS;ENABLE_PHYSICS;ENABLE_SPRITERENDERER_FLIPPING;ENABLE_SPRITES;ENABLE_GRID;ENABLE_TILEMAP;ENABLE_TERRAIN;ENABLE_RAKNET;ENABLE_DIRECTOR;ENABLE_UNET;ENABLE_LZMA;ENABLE_UNITYEVENTS;ENABLE_WEBCAM;ENABLE_WWW;ENABLE_CLOUD_SERVICES_COLLAB;ENABLE_CLOUD_SERVICES_COLLAB_SOFTLOCKS;ENABLE_CLOUD_SERVICES_ADS;ENABLE_CLOUD_HUB;ENABLE_CLOUD_PROJECT_ID;ENABLE_CLOUD_SERVICES_USE_WEBREQUEST;ENABLE_CLOUD_SERVICES_UNET;ENABLE_CLOUD_SERVICES_BUILD;ENABLE_CLOUD_LICENSE;ENABLE_EDITOR_HUB;ENABLE_EDITOR_HUB_LICENSE;ENABLE_WEBSOCKET_CLIENT;ENABLE_DIRECTOR_AUDIO;ENABLE_DIRECTOR_TEXTURE;ENABLE_TIMELINE;ENABLE_EDITOR_METRICS;ENABLE_EDITOR_METRICS_CACHING;ENABLE_NATIVE_ARRAY;ENABLE_SPRITE_MASKING;INCLUDE_DYNAMIC_GI;INCLUDE_GI;ENABLE_MONO_BDWGC;PLATFORM_SUPPORTS_MONO;RENDER_SOFTWARE_CURSOR;INCLUDE_PUBNUB;ENABLE_PLAYMODE_TESTS_RUNNER;ENABLE_VIDEO;ENABLE_RMGUI;ENABLE_PACKMAN;ENABLE_CUSTOM_RENDER_TEXTURE;ENABLE_STYLE_SHEETS;ENABLE_LOCALIZATION;PLATFORM_STANDALONE_WIN;PLATFORM_STANDALONE;UNITY_STANDALONE_WIN;UNITY_STANDALONE;ENABLE_SUBSTANCE;ENABLE_RUNTIME_GI;ENABLE_MOVIES;ENABLE_NETWORK;ENABLE_CRUNCH_TEXTURE_COMPRESSION;ENABLE_UNITYWEBREQUEST;ENABLE_CLOUD_SERVICES;ENABLE_CLOUD_SERVICES_ANALYTICS;ENABLE_CLOUD_SERVICES_PURCHASING;ENABLE_CLOUD_SERVICES_CRASH_REPORTING;ENABLE_EVENT_QUEUE;ENABLE_CLUSTERINPUT;ENABLE_VR;ENABLE_AR;ENABLE_WEBSOCKET_HOST;ENABLE_MONO;NET_2_0_SUBSET;DEVELOPMENT_BUILD;ENABLE_PROFILER;UNITY_ASSERTIONS;UNITY_EDITOR;UNITY_EDITOR_64;UNITY_EDITOR_WIN;ENABLE_NATIVE_ARRAY_CHECKS;UNITY_TEAM_LICENSE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <NoWarn>0169</NoWarn>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>Temp\bin\Release\</OutputPath>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <NoWarn>0169</NoWarn>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.XML" />
    <Reference Include="System.Core" />
    <Reference Include="System.Runtime.Serialization" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="UnityEngine">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/Managed\UnityEngine.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/Managed/UnityEditor.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Assets\old\Boss\Boss1\BigBossMeleeAttack.cs" />
    <Compile Include="Assets\old\Boss\Boss1\BigBossScript.cs" />
    <Compile Include="Assets\old\Boss\Boss1\DefendScript.cs" />
    <Compile Include="Assets\old\Boss\Boss1\Explosion.cs" />
    <Compile Include="Assets\old\Boss\Boss2\Boss2Meele.cs" />
    <Compile Include="Assets\old\Boss\Boss2\Boss2Script.cs" />
    <Compile Include="Assets\old\Boss\Boss2\BossGrounded.cs" />
    <Compile Include="Assets\old\Boss\Boss2\FireBall.cs" />
    <Compile Include="Assets\old\Boss\Boss2\FireScript.cs" />
    <Compile Include="Assets\old\LeveLS\Prolog\Scripts\PrologScript.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\AnkhShield.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\beholdBullets.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\BeholderScript.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\DeathBite.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\DeathGhostMoveTowards.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\DeathHands.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\ElectStrike.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\HealthSphere.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\knifeController.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\MoveTowards.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\PushScript.cs" />
    <Compile Include="Assets\old\Scripts\AttackStuff\RockController.cs" />
    <Compile Include="Assets\old\Scripts\Clock.cs" />
    <Compile Include="Assets\old\Scripts\CoinCollect.cs" />
    <Compile Include="Assets\old\Scripts\Destroy.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\BossesScripts\Forest_houseBoss.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\BossesScripts\ForestBoss.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\EnemyBulletController.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\FlyerEnemy.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\JumpEnemy.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\ShootingEnemy.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\SlimeScript.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\SmthTakeEnemyDMG.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\SmthTakePlayerDMG.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\SorcererScrpit.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\Spikes.cs" />
    <Compile Include="Assets\old\Scripts\Enemy\WalkerShootingEnemy.cs" />
    <Compile Include="Assets\old\Scripts\environment\AltarScript.cs" />
    <Compile Include="Assets\old\Scripts\environment\Box.cs" />
    <Compile Include="Assets\old\Scripts\environment\Chest.cs" />
    <Compile Include="Assets\old\Scripts\environment\ChestScript.cs" />
    <Compile Include="Assets\old\Scripts\environment\FallingPlatform.cs" />
    <Compile Include="Assets\old\Scripts\environment\FireZone.cs" />
    <Compile Include="Assets\old\Scripts\environment\Gate.cs" />
    <Compile Include="Assets\old\Scripts\environment\LadderZone.cs" />
    <Compile Include="Assets\old\Scripts\environment\Loot.cs" />
    <Compile Include="Assets\old\Scripts\environment\ObszarupuszczaniaChest.cs" />
    <Compile Include="Assets\old\Scripts\environment\Read.cs" />
    <Compile Include="Assets\old\Scripts\environment\startAnimationScript.cs" />
    <Compile Include="Assets\old\Scripts\environment\StuffToBreak.cs" />
    <Compile Include="Assets\old\Scripts\environment\Trap.cs" />
    <Compile Include="Assets\old\Scripts\environment\watersplash.cs" />
    <Compile Include="Assets\old\Scripts\Game\CameraFollow.cs" />
    <Compile Include="Assets\old\Scripts\Game\CameraFollowEditor.cs" />
    <Compile Include="Assets\old\Scripts\Game\ElementScript.cs" />
    <Compile Include="Assets\old\Scripts\Game\EQ\Equipment.cs" />
    <Compile Include="Assets\old\Scripts\Game\EQ\Item.cs" />
    <Compile Include="Assets\old\Scripts\Game\EQ\ItemDataBase.cs" />
    <Compile Include="Assets\old\Scripts\Game\EqMenu.cs" />
    <Compile Include="Assets\old\Scripts\Game\Equipment\CanvasEkwipunek.cs" />
    <Compile Include="Assets\old\Scripts\Game\Equipment\ElementEq.cs" />
    <Compile Include="Assets\old\Scripts\Game\Equipment\EqElementType.cs" />
    <Compile Include="Assets\old\Scripts\Game\Equipment\itemDataBase.cs" />
    <Compile Include="Assets\old\Scripts\Game\Equipment\ObszarUpuszczenia.cs" />
    <Compile Include="Assets\old\Scripts\Game\ExpSlider.cs" />
    <Compile Include="Assets\old\Scripts\Game\gameMaster.cs" />
    <Compile Include="Assets\old\Scripts\Game\gameMenager.cs" />
    <Compile Include="Assets\old\Scripts\Game\HUD.cs" />
    <Compile Include="Assets\old\Scripts\Game\Paralaxing.cs" />
    <Compile Include="Assets\old\Scripts\Game\PauseMenu.cs" />
    <Compile Include="Assets\old\Scripts\Game\PowerUIScript.cs" />
    <Compile Include="Assets\old\Scripts\Game\Resolution.cs" />
    <Compile Include="Assets\old\Scripts\Game\SkillMenu.cs" />
    <Compile Include="Assets\old\Scripts\Game\tipsScript.cs" />
    <Compile Include="Assets\old\Scripts\Key.cs" />
    <Compile Include="Assets\old\Scripts\Player\AttackTrigger.cs" />
    <Compile Include="Assets\old\Scripts\Player\PlayerAttack.cs" />
    <Compile Include="Assets\old\Scripts\Player\PlayerController.cs" />
    <Compile Include="Assets\old\Scripts\Sliding.cs" />
    <Compile Include="Assets\old\Scripts\VillageScripts\Smith.cs" />
    <Compile Include="Assets\old\Scripts\yellowGate.cs" />
    <Compile Include="Assets\pixelboy.cs" />
    <Compile Include="Assets\Scripts\Destroy_obj.cs" />
    <Compile Include="Assets\Scripts\Envoirment\Door.cs" />
    <Compile Include="Assets\Scripts\Envoirment\Falling_Platform.cs" />
    <Compile Include="Assets\Scripts\Game\Game_Master.cs" />
    <Compile Include="Assets\Scripts\Game\LoadingScreenManager.cs" />
    <Compile Include="Assets\Scripts\Game\LoadTargetScript.cs" />
    <Compile Include="Assets\Scripts\Game\Saveing.cs" />
    <Compile Include="Assets\Scripts\Game\SoundHolder.cs" />
    <Compile Include="Assets\Scripts\Main menu scripts\GameMasterMainMenu.cs" />
    <Compile Include="Assets\Scripts\Main menu scripts\LoadLeveL.cs" />
    <Compile Include="Assets\Scripts\Main menu scripts\MainMenu.cs" />
    <Compile Include="Assets\Scripts\Player\GroundCheck.cs" />
    <Compile Include="Assets\Scripts\Player\Player_Attack.cs" />
    <Compile Include="Assets\Scripts\Player\Player_Controller.cs" />
    <Compile Include="Assets\Scripts\Saving_Script.cs" />
    <Reference Include="UnityEngine.UI">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/GUISystem/UnityEngine.UI.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.UI">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/GUISystem/Editor/UnityEditor.UI.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.Networking">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/Networking/UnityEngine.Networking.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Networking">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/Networking/Editor/UnityEditor.Networking.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.TestRunner">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/TestRunner/Editor/UnityEditor.TestRunner.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.TestRunner">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/TestRunner/UnityEngine.TestRunner.dll</HintPath>
    </Reference>
    <Reference Include="nunit.framework">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/TestRunner/net35/unity-custom/nunit.framework.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.Timeline">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/Timeline/RuntimeEditor/UnityEngine.Timeline.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Timeline">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/Timeline/Editor/UnityEditor.Timeline.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.TreeEditor">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/TreeEditor/Editor/UnityEditor.TreeEditor.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.UIAutomation">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UIAutomation/UnityEngine.UIAutomation.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.UIAutomation">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UIAutomation/Editor/UnityEditor.UIAutomation.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.GoogleAudioSpatializer">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnityGoogleAudioSpatializer/Editor/UnityEditor.GoogleAudioSpatializer.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.GoogleAudioSpatializer">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnityGoogleAudioSpatializer/RuntimeEditor/UnityEngine.GoogleAudioSpatializer.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.HoloLens">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnityHoloLens/Editor/UnityEditor.HoloLens.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.HoloLens">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnityHoloLens/RuntimeEditor/UnityEngine.HoloLens.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.SpatialTracking">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnitySpatialTracking/Editor/UnityEditor.SpatialTracking.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.SpatialTracking">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnitySpatialTracking/RuntimeEditor/UnityEngine.SpatialTracking.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.VR">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/UnityExtensions/Unity/UnityVR/Editor/UnityEditor.VR.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Graphs">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/Managed/UnityEditor.Graphs.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Android.Extensions">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/PlaybackEngines/AndroidPlayer/UnityEditor.Android.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.WindowsStandalone.Extensions">
      <HintPath>D:/Programy/unity/Unity 2017/Editor/Data/PlaybackEngines/windowsstandalonesupport/UnityEditor.WindowsStandalone.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.Advertisements">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.ads@2.0.3/UnityEngine.Advertisements.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Advertisements">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.ads@2.0.3/Editor/UnityEditor.Advertisements.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.Analytics">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.analytics@2.0.13/UnityEngine.Analytics.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Analytics">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.analytics@2.0.13/Editor/UnityEditor.Analytics.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.Purchasing">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.purchasing@0.0.19/UnityEngine.Purchasing.dll</HintPath>
    </Reference>
    <Reference Include="UnityEditor.Purchasing">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.purchasing@0.0.19/Editor/UnityEditor.Purchasing.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.StandardEvents">
      <HintPath>C:/Users/komp/AppData/LocalLow/Unity/cache/packages/packages.unity.com/com.unity.standardevents@1.0.10/UnityEngine.StandardEvents.dll</HintPath>
    </Reference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
</Project>