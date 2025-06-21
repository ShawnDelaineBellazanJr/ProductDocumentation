using Microsoft.SemanticKernel;

namespace Steps;

public sealed class GetProductInfoStep : KernelProcessStep
{
    [KernelFunction]
    public string GetProductInfo(string productName = "GlowBrew")
    {
        Console.WriteLine($"🔍 GetProductInfoStep: Retrieving information for product: {productName}");
        
        // Simulate dynamic data retrieval based on product name
        var productInfo = GenerateProductInfo(productName);
        
        Console.WriteLine($"✅ GetProductInfoStep: Retrieved information for {productName}");
        return productInfo;
    }
    
    private string GenerateProductInfo(string productName)
    {
        // Dynamic product information based on input
        return productName.ToLower() switch
        {
            "glowbrew" => """
                Product Description:
                GlowBrew is a revolutionary AI driven coffee machine with industry leading number of LEDs and programmable light shows. The machine is also capable of brewing coffee and has a built in grinder.

                Product Features:
                1. **Luminous Brew Technology**: Customize your morning ambiance with programmable LED lights that sync with your brewing process.
                2. **AI Taste Assistant**: Learns your taste preferences over time and suggests new brew combinations to explore.
                3. **Gourmet Aroma Diffusion**: Built-in aroma diffusers enhance your coffee's scent profile, energizing your senses before the first sip.

                Troubleshooting:
                - **Issue**: LED Lights Malfunctioning
                    - **Solution**: Reset the lighting settings via the app. Ensure the LED connections inside the GlowBrew are secure. Perform a factory reset if necessary.
                """,
            
            "smartfridge" => """
                Product Description:
                SmartFridge is an intelligent refrigerator with AI-powered food management, automated inventory tracking, and smart temperature control.

                Product Features:
                1. **AI Food Recognition**: Automatically identifies and categorizes food items using computer vision.
                2. **Expiry Tracking**: Monitors expiration dates and sends alerts for items nearing expiry.
                3. **Smart Shopping Lists**: Generates shopping lists based on inventory and consumption patterns.
                4. **Energy Optimization**: AI-driven temperature control for optimal energy efficiency.

                Troubleshooting:
                - **Issue**: Temperature Fluctuations
                    - **Solution**: Check door seals, ensure proper ventilation, and reset temperature settings via the mobile app.
                """,
            
            "quantumphone" => """
                Product Description:
                QuantumPhone is the world's first quantum-secured smartphone with advanced AI capabilities and holographic display technology.

                Product Features:
                1. **Quantum Encryption**: Unbreakable quantum-secured communications and data storage.
                2. **Holographic Display**: 3D holographic projections for immersive user experience.
                3. **AI Personal Assistant**: Advanced AI that learns and adapts to user behavior patterns.
                4. **Quantum Computing Integration**: Access to quantum computing resources for complex tasks.

                Troubleshooting:
                - **Issue**: Holographic Display Not Working
                    - **Solution**: Ensure adequate lighting, check holographic projector alignment, and restart the device.
                """,
            
            _ => $"""
                Product Description:
                {productName} is a cutting-edge product with advanced features and innovative technology.

                Product Features:
                1. **Advanced Technology**: State-of-the-art features and capabilities.
                2. **User-Friendly Design**: Intuitive interface and easy-to-use controls.
                3. **Smart Integration**: Seamless connectivity with other devices and services.
                4. **Performance Optimization**: Optimized for maximum efficiency and reliability.

                Troubleshooting:
                - **Issue**: General Performance Issues
                    - **Solution**: Restart the device, check for updates, and ensure proper setup and configuration.
                """
        };
    }
}
