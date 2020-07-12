# LovelyLogs
---
LovelyLogs is a logging framework for use in C# within the .NET framework.

## Installation
---
### Install Nuget package via command line

macOS/Linux

```bash
> dotnet add package LovelyLogs --version 1.0.0
```

Package Manager Console (Windows):

```cmd
PM> Install-Package LovelyLogs -Version 1.0.0
```

## Usage
---
### Setup

All you need to do to start logging is create a reference to `ILog` by calling `LogManager.GetLogger(string loggerName)`. The `loggerName` parameter is normally the name of the calling class where the logger is created. This will create a logger with that will record log messages of all levels to the console by default.
```c
class LoggingTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggingTest).ToString());

        static void Main(string[] args)
        {
            log.Debug("Debug message");
            log.Error("Error message");
            log.Info("Info message");
            log.Warn("Warning message");
            log.Fatal("Fatal message");
        }
    }
```

### Appenders

Appenders detemine where the logs are recorded. They are added using the `AddAppender(Appender appender)` method. There are three types of `Appender`: `ConsoleAppender`, `FileAppender` and `RollingFileAppender`. Every logger comes with a `ConsoleAppender` by default, which can be removed using the method `RemoveAppender(Appender appender)`. The `FileAppender` is constructed with the directory of a .txt file you would like to use to record all logs to, and the `RollingFileAppender` is constructed with a directory that will begin logging in a .txt titled with the current date and resume logging in a new file when the date changes.

### Formatting logs

Using the loggers `SetLogFormat(string format)` method we can change the format using up to five different parameters delimited by the `%` character.

```c
log.SetLogFormat("%date{yyyy-MM-dd H:mm:ss.ffff} %logLevel %logger %message%newLine");
```

`%date` - The timestamp of the log. Can be formatted using composite formatting by adding a custom format using the same syntax used by the `DateTime` type. For example, `%date{yyyy-MM-dd H:mm:ss.ffff}`. More information can be found here: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings

`%logLevel` - The associated log level, indicating importance.

`%logger` - The name of the logger (usually the class) that logged the message.

`%message` - The contents of the log message.

`%newLine` - A new line.

The default format for logs is `%date{yyyy-MM-dd H:mm:ss.ffff} %logLevel %logger %message%newLine`.

### Setting log levels

Setting the log level determines which types of logs the logger will record. There are seven log levels that are ordered as following:
`ALL` < `DEBUG` < `INFO` < `WARN` < `ERROR` < `FATAL` < `OFF` 

```c
log.SetLogLevel(LogLevel.WARN);

log.Debug("Debug information");
log.Info("62.3% increase in speed");
log.Warn("Potentially harmful situation!");
log.Error("Normal execution may not be possible.");
log.Fatal("Application may terminate!");
```

For example, the above configuration will result in the following output:
```
2020-07-11 11:42:16.0966 WARN LoggingTest.LoggingTest Potentially harmful situation!
2020-07-11 11:42:16.1821 ERROR LoggingTest.LoggingTest Normal execution may not be possible. 
2020-07-11 11:42:16.1830 FATAL LoggingTest.LoggingTest Application may terminate!
```


## Implementation Choices
---
### The LogManager and Multiple Loggers
I decided to use a `LogManager` class so that multiple loggers could be easily added and managed (one for each class that requires logging functionality). I implemented the class using the singleton pattern as it made the process significantly easier and would was easier to make it thread safe, as potentially a large number of classes could use it. The `LogManager` is laziliy initialised with the `Lazy<T>` class allowing for safe access from multiple threads.


### Configuration

As this is a relatively simple logging framework with only a few configuration options, I decided that configuring the loggers through code made the most sense, as opposed to using XML files. The log format can be fairly easily customised using composite formatting for the date and simply ordering the other elements of the logs. The framework can be setup to print out to the console with default settings in only one line of code, and only requires three more lines of code to output logs to a file, set a custom format, and set the log level of the logger.

### RollingFileAppender

I chose to implement a rolling file appender as logging to one file may be appropriate for small applications, however one log file can quickly become flooded with logs and become difficult to maintain. I have implemented a simple rolling file appender that resumes logging in a new file when the date changes. Separate logs named by date are a simple addition that vastly improve the developers' ability to organise logs.


