using System;

public static class TimerStoradge {
    public static DateTime startTime, pauseTime;
    public static TimeSpan pauseSpan = TimeSpan.Zero;

    public static bool isStart = false;
    public static bool isTimer = false;
    public static bool isAfterPause = false;
    public static bool isFirstTime = true;
}
