namespace WeatherForecastTest.Hosting
{
    public static class HostingExtensions
    {
        /// <summary>
        /// Checks if the current host environment name is not production <see cref="Environments.Production"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is not <see cref="Environments.Production"/>, otherwise false.</returns>
        public static bool IsNotProduction(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null) throw new ArgumentNullException(nameof(hostEnvironment));

            return !hostEnvironment.IsEnvironment(Environments.Production);
        }
    }
}