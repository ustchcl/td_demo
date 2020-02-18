

namespace Hong {
    public class BaseFunctions {
        private static BaseFunctions _instance;
        private BaseFunctions() {}

        public static BaseFunctions Instance {
            get { 
                if (_instance == null) {
                    _instance = new BaseFunctions();
                }
                return _instance;
            }
        }

        public float add(float a, float b) {
            return a + b;
        }


    }
}