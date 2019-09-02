namespace Core.Managers
{
    public interface IDistributable
    {
        /// <summary>
        /// Number of initialization generation for system or manager. The more value - the later system or manager will be initialized.
        /// </summary>
        int InitializationGeneration { get; }

        /// <summary>
        /// Initializes system or manager.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Clears all links in class and disables class for using
        /// </summary>
        void Dispose();
    }
}