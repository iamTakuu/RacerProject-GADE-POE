public static class CustomExtensions
    {
        public static float Remap(this float value, 
            float valueRangeMin, float valueRangeMax, 
            float newRangeMin, float newRangeMax) 
        {
            return (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;
        }

    }