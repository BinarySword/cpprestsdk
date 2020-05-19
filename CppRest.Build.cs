// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class CppRest : ModuleRules
{
    public CppRest(ReadOnlyTargetRules Target) : base(Target)
    {
        Type = ModuleType.External;

        PublicIncludePaths.AddRange(
            new[]
            {
                "ThirdParty/CppRest/Release/include", //Relative To "Source" Folder
                "/usr/local/include"
                // ... add public include paths required here ...
            }
        );


        PrivateIncludePaths.AddRange(
            new[]
            {
                "ThirdParty/CppRest/Release/include", //Relative To "Source" Folder
                "/usr/local/include"
                // ... add other private include paths required here ...
            }
        );


        PublicDependencyModuleNames.AddRange(
            new[]
            {
                "Core",
                "CoreUObject",
                "Engine"
                // ... add other public dependencies that you statically link with here ...
            }
        );


        PrivateDependencyModuleNames.AddRange(
            new[]
            {
                "UnrealEd",
                "LevelEditor",
                "Slate", "SlateCore",
                "UMG"
                // ... add private dependencies that you statically link with here ...
            }
        );

        DynamicallyLoadedModuleNames.AddRange(
            new string[]
            {
            }
        );

        var CppRestLibraryPath = "";
        var BoostLibraryPath = "";
        var SystemLibraryPath = "";
        var OpenSSLLibraryPath = "";

        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            CppRestLibraryPath = ModuleDirectory + "/Binaries/x64/Release/Windows";
        }

        else if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            SystemLibraryPath = "/usr/lib";
            CppRestLibraryPath = ModuleDirectory + "/Binaries/x64/Release/Mac";
            BoostLibraryPath = "/usr/local/Cellar/boost/1.60.0_2/lib";
            OpenSSLLibraryPath = "/usr/local/Cellar/openssl/1.0.2d_1/lib";
        }

        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            PublicLibraryPaths.AddRange(
                new[]
                {
                    CppRestLibraryPath
                }
            );

            PublicAdditionalLibraries.AddRange(
                new[]
                {
                    "cpprest140_2_5.lib",
                    "Winhttp.lib",
                    "httpapi.lib",
                    "bcrypt.lib",
                    "crypt32.lib"
                }
            );

            Definitions.AddRange(
                new[]
                {
                    "_NO_PPLXIMP=1",
                    "_NO_ASYNCRTIMP=1",
                    "WINSOCK_DEPRECATED_NO_WARNINGS=1",
                    "_ASYNCRT_EXPORT=1",
                    "_PPLX_EXPORT=1",
                    "WIN32=1",
                    "_MBCS=1",
                    "_USRDLL=1"
                }
            );
        }

        else if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            PublicLibraryPaths.AddRange(
                new[]
                {
                    CppRestLibraryPath,
                    BoostLibraryPath,
                    SystemLibraryPath,
                    OpenSSLLibraryPath
                }
            );

            PublicAdditionalLibraries.AddRange(
                new[]
                {
                    CppRestLibraryPath + "/libcpprest.a",
                    BoostLibraryPath + "/libboost_locale-mt.a",
                    BoostLibraryPath + "/libboost_chrono-mt.a",
                    BoostLibraryPath + "/libboost_system-mt.a",
                    BoostLibraryPath + "/libboost_thread-mt.a",
                    OpenSSLLibraryPath + "/libcrypto.a",
                    OpenSSLLibraryPath + "/libssl.a",
                    SystemLibraryPath + "/libiconv.dylib"
                }
            );

            Definitions.AddRange(
                new[]
                {
                    "_NO_PPLXIMP=1",
                    "_NO_ASYNCRTIMP=1"
                }
            );
        }

        //Force No Debug Information In Development Builds.
        BuildConfiguration.bOmitPCDebugInfoInDevelopment = true;
        
        // Add LTCG to the link command line to improve linker performance.
        BuildConfiguration.bAllowLTCG = true;
    }
}