﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ocuda.Ops.Models.Entities
{
    public class Section : Abstract.BaseEntity
    {
        [MaxLength(64)]
        public string Path { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Icon { get; set; }

        [DisplayName("Sort Order")]
        public int SortOrder { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Show in Navigation Bar?")]
        public bool IsNavigation { get; set; }

        [DisplayName("Video Embed URL")]
        public string EmbeddedVideo { get; set; }

        [NotMapped]
        public bool IsDefault
        {
            get => string.IsNullOrWhiteSpace(Path);
        }
    }
}
