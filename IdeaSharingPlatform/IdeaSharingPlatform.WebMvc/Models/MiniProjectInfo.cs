using IdeaSharingPlatform.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaSharingPlatform.WebMvc.Models
{
    public class MiniProjectInfo
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectBlurb { get; set; }
        public DateTime ProjectCreationDate { get; set; }
        public int ProjectsCategoryId { get; set; }
        public int ProjectOwnerID { get; set; }
        public int ProjectLikeNumber { get; set; }
        public int ProjectSaveNumber { get; set; }
        public virtual Categories ProjectsCategory { get; set; }
        public virtual Users ProjectOwner { get; set; }
    }
}