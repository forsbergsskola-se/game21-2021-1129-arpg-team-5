namespace GameDevTV.Utils
{
    /// <summary>
    /// Container class that wraps a value and ensures initialisation is 
    /// called just before first use.
    /// </summary>
    public class LazyValue<T>
    {
        private T _value;
        private bool _initialized = false;
        private InitializerDelegate _initializer;

        public delegate T InitializerDelegate();

        /// <summary>
        /// Setup the container but don't initialise the value yet.
        /// </summary>
       
        public LazyValue(InitializerDelegate initializer)
        {
            _initializer = initializer;
        }

        /// <summary>
        /// Get or set the contents of this container.
        /// </summary>
        /// <remarks>
        /// Note that setting the value before initialisation will initialise 
        /// the class.
        /// </remarks>
        public T value
        {
            get
            {
                
                ForceInit();// Ensure we init before returning a value.
                return _value;
            }
            set
            {
               
                _initialized = true; // Don't use default init anymore.
                _value = value;
            }
        }

        /// <summary>
        /// Force the intialisastion of the value via the delegate.
        /// </summary>
        public void ForceInit()
        {
            if (!_initialized)
            {
                _value = _initializer();
                _initialized = true;
            }
        }
    }
}