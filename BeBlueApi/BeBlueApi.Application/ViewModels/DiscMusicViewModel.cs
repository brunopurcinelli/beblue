using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class DiscMusicViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome do Disco")]
        public string Name { get; set; }

        [DisplayName("Id do Gênero")]
        public Guid IdGender { get; set; }

        [DisplayName("Id do Spotify")]
        public string IdSpotify { get; private set; }

        [DisplayName("Nome do Artista")]
        public string ArtistName { get; private set; }

        [DisplayName("Grupo do Album")]
        public string AlbumGroup { get; private set; }

        [DisplayName("Valor Unitário")]
        public decimal Price { get; set; }
    }
}
