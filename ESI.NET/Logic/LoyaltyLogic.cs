﻿using ESI.NET.Models.Loyalty;
using ESI.NET.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static ESI.NET.EsiRequest;

namespace ESI.NET.Logic
{
    public class LoyaltyLogic
    {
        private HttpClient _client;
        private ESIConfig _config;
        private AuthorizedCharacterData _data;
        private int character_id;

        public LoyaltyLogic(HttpClient client, ESIConfig config, AuthorizedCharacterData data = null)
        {
            _client = client;
            _config = config;
            _data = data;

            if (data != null)
                character_id = data.CharacterID;
        }

        /// <summary>
        /// /loyalty/stores/{corporation_id}/offers/
        /// </summary>
        /// <returns></returns>
        public async Task<EsiResponse<List<Offer>>> Offers(int corporation_id)
            => await Execute<List<Offer>>(_client, _config, RequestSecurity.Public, RequestMethod.GET, $"/loyalty/stores/{corporation_id}/offers/");

        /// <summary>
        /// /characters/{character_id}/loyalty/points/
        /// </summary>
        /// <returns></returns>
        public async Task<EsiResponse<List<Points>>> Points()
            => await Execute<List<Points>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/characters/{character_id}/loyalty/points/", token: _data.Token);
    }
}