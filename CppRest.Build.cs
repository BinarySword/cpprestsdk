// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.
using UnrealBuildTool;

public class CppRest : ModuleRules
{
    public CppRest(TargetInfo Target)
    {

        Type = ModuleType.External;

        PublicIncludePaths.AddRange(
            new string[]
            {
                "ThirdParty/CppRest/Release/include",//Relative To "Source" Folder
                "/usr/local/include"
				// ... add public include paths required here ...
			}
            );


        PrivateIncludePaths.AddRange(
            new string[]
            {
                "ThirdParty/CppRest/Release/include",//Relative To "Source" Folder
                "/usr/local/include"
				// ... add other private include paths required here ...
			}
            );


        PublicDependencyModuleNames.AddRange(
            new string[]
            {
                "Core",
                "CoreUObject",
                "Engine"
				// ... add other public dependencies that you statically link with here ...
			}
            );


        PrivateDependencyModuleNames.AddRange(
            new string[]
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

        string CppRestLibraryPath = "";
        string BoostLibraryPath = "";
        string SystemLibraryPath = "";
        string OpenSSLLibraryPath = "";

        if (Target.Platform == UnrealTargetPlatform.Win64)
            CppRestLibraryPath = ModuleDirectory + "/Binaries/x64/Release/Windows";

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
            new string[]
            {
                CppRestLibraryPath
            }
            );

            PublicAdditionalLibraries.AddRange(
                new string[]
                {
                    "cpprest140_2_5.lib",
                    "Winhttp.lib",
                    "httpapi.lib",
                    "bcrypt.lib",
                    "crypt32.lib"
                }
                );

            Definitions.AddRange(
                new string[]
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
            new string[]
            {
                CppRestLibraryPath,
                BoostLibraryPath,
                SystemLibraryPath,
                OpenSSLLibraryPath
            }
            );

            PublicAdditionalLibraries.AddRange(
                new string[]
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
                new string[]
                {
                    "_NO_PPLXIMP=1",
                    "_NO_ASYNCRTIMP=1"
                }
                );
        }

        //Force No Debug Information In Development Builds.
        BuildConfiguration.bOmitPCDebugInfoInDevelopment = true;
    }
}