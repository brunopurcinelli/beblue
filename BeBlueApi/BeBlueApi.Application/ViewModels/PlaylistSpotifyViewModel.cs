using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeBlueApi.Application.ViewModels
{
    public class PlaylistSpotifyViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Owner")]
        public string Owner { get; set; }

        [DisplayName("No. of tracks")]
        public int NumberOfTracks { get; set; }

        [DisplayName("Is Public")]
        public bool? IsPublic { get; set; }

        [DisplayName("Is Collaborative")]
        public bool IsCollaborative { get; set; }
    }
}
