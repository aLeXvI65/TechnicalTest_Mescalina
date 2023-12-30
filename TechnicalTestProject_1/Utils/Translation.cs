namespace TechnicalTestProject_1.Utils {
    public static class Translation {

        static List<string> englishList = new List<string> { 
            "Name", "place"
        };

        static List<string> spanishList = new List<string> {
            "Nombre",
            "Lugar"
        };

        public static string translate(string value) {
            int i = englishList.IndexOf(value);
            return spanishList[i];
        }

    }
}
