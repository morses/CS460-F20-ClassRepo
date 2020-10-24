using System.Collections.Generic;

namespace PartyInvites.Models {
    public static class Repository {
        private static List<GuestResponse_old> responses = new List<GuestResponse_old>();

        public static IEnumerable<GuestResponse_old> Responses => responses;

        public static void AddResponse(GuestResponse_old response) {
            responses.Add(response);
        }
    }
}
