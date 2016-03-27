using System;
using System.ComponentModel.DataAnnotations;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name="邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

		[Required]
		public string Roles { get; set; }
    }


    public class VehicleInfoModel
    {
        [Required]
        [Display(Name = "车辆品牌ID")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "车辆品牌")]
        public Brand VehicleBrand { get; set; }

        [Required]
        [Display(Name = "车辆类型ID")]
        public int VehicleTypeId { get; set; }

        [Required]
        [Display(Name = "车辆类型")]
        public VehicleType VehicleType { get; set; }

        [Required]
        [Display(Name = "车牌号")]
        public string Plate { get; set; }

        [Required]
        [Display(Name = "出厂时间")]
        public DateTime DeliveryTtime { get; set; }

        [Required]
        [Display(Name = "购买日期")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Display(Name = "投保状态")]
        public bool InsuranceStatus { get; set; }

        [Required]
        [Display(Name = "车内消防设施情况")]
        public FireCarType FireCar { get; set; }

        [Required]
        [Display(Name = "照明设施情况")]
        public LightConditionType LightCondition { get; set; }

        [Required]
        [Display(Name = "车牌是否完好")]
        public PlateType PlateIsIntact { get; set; }

        [Required]
        [Display(Name = "最后一次车检时间")]
        public DateTime? LastInspectionTime { get; set; }

        [Required]
        [Display(Name = "下一次车检日期")]
        public DateTime? NextInspectionTime { get; set; }

        [Required]
        [Display(Name = "车主信息ID")]
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "车主信息")]
        public OwnerInfo Owner { get; set; }

    }

}
