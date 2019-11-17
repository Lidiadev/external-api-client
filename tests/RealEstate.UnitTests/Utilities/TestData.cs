namespace RealEstate.UnitTests.Utilities
{
    public static class TestData
    {
        public static string ApiReponse
            =>@"{
                ""Paging"": {
                    ""AantalPaginas"": 1,
                    ""HuidigePagina"": 1
                    },
                ""Objects"": [
                    {
                        ""GlobalId"": 100001,
                        ""Id"": ""1"",
                        ""MakelaarId"": 1111,
                        ""MakelaarNaam"": ""Agent 1""
                    },
                    {
                        ""GlobalId"": 100002,
                        ""Id"": ""2"",
                        ""MakelaarId"": 2222,
                        ""MakelaarNaam"": ""Agent 2""
                    },
                    {
                        ""GlobalId"": 100003,
                        ""Id"": ""3"",
                        ""MakelaarId"": 3333,
                        ""MakelaarNaam"": ""Agent 3""
                    }
                ]
            }";
    }
}
