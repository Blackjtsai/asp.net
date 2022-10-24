# 2/21 .Net 6.0
=============
# VSCODE 安裝 C#
# 安裝 SDK .Net6.0 SDK
# 建立專案
+ dotnet new mvc -n EventChart_AP --no-https  -f net6.0
+ dotnet new mvc -n EventChart_Web --no-https  -f net6.0
+ dotnet new classlib -n EventChart_Core -f net6.0

# 設定(新增修改) .vscode/launch.json
```
 {
      "name": "EventChart_AP",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/EventChart_AP/bin/Debug/net6.0/EventChart_AP.dll",
      "args": [],
      "cwd": "${workspaceFolder}/EventChart_AP",
      "console": "internalConsole",
      "stopAtEntry": false
    },
```
# 設定(新增修改) .vscode/tasks.json
```
{
      "label": "build_web",
      "command": "dotnet",
      "type": "process",
      "args": [
        "build",
        "${workspaceFolder}/EventChart_Web/EventChart_Web.csproj",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "problemMatcher": "$msCompile"
    },
```
# 依賴
-   dotnet add EventChart_AP/EventChart_AP.csproj reference EventChart_Core/EventChart_Core.csproj

# run (test)

# route
-   Program.cs
```
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

# set檔案
+  webconfig -> appsetting 
+  Program.cs
+  EventChart_AP.csproj / EventChart_Core.csproj 中 停用 <Nullable>enable</Nullable> -> <Nullable>disable</Nullable>


# Nuget 引用資源
https://www.nuget.org/
+   dotnet add package MySql.Data --version 8.0.28
+   dotnet add package Newtonsoft.Json --version 13.0.1
+   dotnet add package System.Data.SqlClient --version 4.8.3
+   dotnet add package Dapper --version 2.0.123  

# 連DB
+ DBFactory.cs
+ PMPDBFactory.cs
+ DAOEmpy.cs


# 環境設定
# server 需安裝 dotnet runtime ( 或 ＳＤＫ 但檔案大 https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
+ 發布 EventChart_AP
  + cd/EventChart_AP
  + dotnet publish -o ../EventChart_AP_publish
+ 本機 RUN (自建 kestrel server ）
  + dotnet EventChart_AP.dll --urls http://+:5555

+ server 端要有watch dll 功能





# LinQ
```
// The Three Parts of a LINQ Query:
        // 1. Data source.
        int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

        // 2. Query creation.
        // numQuery is an IEnumerable<int>
        var numQuery =
            from num in numbers
            where (num % 2) == 0
            select num;

        // 3. Query execution.
        foreach (int num in numQuery)
        {
            Console.Write("{0,1} ", num);
        }
        
List<int> numQuery2 =
    (from num in numbers
     where (num % 2) == 0
     select num).ToList();

// or like this:
// numQuery3 is still an int[]

var numQuery3 =
    (from num in numbers
     where (num % 2) == 0
     select num).ToArray();
````

