using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Entities;

namespace Bagahe.Interfaces
{
    public interface IRestService
    {
        Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserInput input);
        GetBagInfoResponse GetBagInfo(GetBagInfoInput input);
        TrackResponse GetTrackBagScanPoints(BagTrackUserInput input);
        Task<TrackResponse> TrackBagScanPoints(BagTrackUserInput input);
        Task<String> Consume();

        Dictionary<String, String> Parameters { set; }

        String WebMethod { set; }
    }
}
