using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Text label;
    private void Awake()
    {
        label.text =
            $"ОС: {SystemInfo.operatingSystem} \n" +
            $"Тип: {SystemInfo.deviceType} \n" +
            $"Модель: {SystemInfo.deviceModel} \n" +
            $"CPU: {SystemInfo.processorType} \n" +
            $"GPU: {SystemInfo.graphicsDeviceName} \n" +
            $"RAM: {SystemInfo.systemMemorySize}Mb\n" +
            $"Батарея: {SystemInfo.batteryLevel * 100}%\n";
    }
}
