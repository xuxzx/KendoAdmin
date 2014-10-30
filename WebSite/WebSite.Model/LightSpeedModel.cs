using System;

using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Validation;
using Mindscape.LightSpeed.Linq;

namespace WebSite.Model
{
  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class Admin : Entity<int>
  {
    #region Fields
  
    [ValidateLength(0, 50)]
    private string _loginName;
    [ValidateLength(0, 50)]
    private string _password;
    [ValidateLength(0, 50)]
    private string _nickName;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the LoginName entity attribute.</summary>
    public const string LoginNameField = "LoginName";
    /// <summary>Identifies the Password entity attribute.</summary>
    public const string PasswordField = "Password";
    /// <summary>Identifies the NickName entity attribute.</summary>
    public const string NickNameField = "NickName";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Admin")]
    private readonly EntityCollection<NewsInfo> _newsInfos = new EntityCollection<NewsInfo>();
    [ReverseAssociation("Admin")]
    private readonly EntityCollection<PicInfo> _picInfos = new EntityCollection<PicInfo>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<NewsInfo> NewsInfos
    {
      get { return Get(_newsInfos); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<PicInfo> PicInfos
    {
      get { return Get(_picInfos); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string LoginName
    {
      get { return Get(ref _loginName, "LoginName"); }
      set { Set(ref _loginName, value, "LoginName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Password
    {
      get { return Get(ref _password, "Password"); }
      set { Set(ref _password, value, "Password"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string NickName
    {
      get { return Get(ref _nickName, "NickName"); }
      set { Set(ref _nickName, value, "NickName"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class NewsInfo : Entity<int>
  {
    #region Fields
  
    [ValidateLength(0, 200)]
    private string _title;
    [ValidateLength(0, 8000)]
    private string _newsContent;
    private int _visitCount;
    private System.DateTime _writeTime;
    private int _adminId;
    private int _newsTypeId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Title entity attribute.</summary>
    public const string TitleField = "Title";
    /// <summary>Identifies the NewsContent entity attribute.</summary>
    public const string NewsContentField = "NewsContent";
    /// <summary>Identifies the VisitCount entity attribute.</summary>
    public const string VisitCountField = "VisitCount";
    /// <summary>Identifies the WriteTime entity attribute.</summary>
    public const string WriteTimeField = "WriteTime";
    /// <summary>Identifies the AdminId entity attribute.</summary>
    public const string AdminIdField = "AdminId";
    /// <summary>Identifies the NewsTypeId entity attribute.</summary>
    public const string NewsTypeIdField = "NewsTypeId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("NewsInfos")]
    private readonly EntityHolder<Admin> _admin = new EntityHolder<Admin>();
    [ReverseAssociation("NewsInfos")]
    private readonly EntityHolder<NewsType> _newsType = new EntityHolder<NewsType>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public Admin Admin
    {
      get { return Get(_admin); }
      set { Set(_admin, value); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public NewsType NewsType
    {
      get { return Get(_newsType); }
      set { Set(_newsType, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string Title
    {
      get { return Get(ref _title, "Title"); }
      set { Set(ref _title, value, "Title"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string NewsContent
    {
      get { return Get(ref _newsContent, "NewsContent"); }
      set { Set(ref _newsContent, value, "NewsContent"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public int VisitCount
    {
      get { return Get(ref _visitCount, "VisitCount"); }
      set { Set(ref _visitCount, value, "VisitCount"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime WriteTime
    {
      get { return Get(ref _writeTime, "WriteTime"); }
      set { Set(ref _writeTime, value, "WriteTime"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Admin" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int AdminId
    {
      get { return Get(ref _adminId, "AdminId"); }
      set { Set(ref _adminId, value, "AdminId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="NewsType" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int NewsTypeId
    {
      get { return Get(ref _newsTypeId, "NewsTypeId"); }
      set { Set(ref _newsTypeId, value, "NewsTypeId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class NewsType : Entity<int>
  {
    #region Fields
  
    [ValidateLength(0, 50)]
    private string _newsTypeName;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the NewsTypeName entity attribute.</summary>
    public const string NewsTypeNameField = "NewsTypeName";


    #endregion
    
    #region Relationships

    [ReverseAssociation("NewsType")]
    private readonly EntityCollection<NewsInfo> _newsInfos = new EntityCollection<NewsInfo>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<NewsInfo> NewsInfos
    {
      get { return Get(_newsInfos); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string NewsTypeName
    {
      get { return Get(ref _newsTypeName, "NewsTypeName"); }
      set { Set(ref _newsTypeName, value, "NewsTypeName"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class PicInfo : Entity<int>
  {
    #region Fields
  
    private string _title;
    private string _link;
    private string _pic;
    private System.DateTime _writeTime;
    private int _orderId;
    private int _adminId;
    private int _picTypeId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Title entity attribute.</summary>
    public const string TitleField = "Title";
    /// <summary>Identifies the Link entity attribute.</summary>
    public const string LinkField = "Link";
    /// <summary>Identifies the Pic entity attribute.</summary>
    public const string PicField = "Pic";
    /// <summary>Identifies the WriteTime entity attribute.</summary>
    public const string WriteTimeField = "WriteTime";
    /// <summary>Identifies the OrderId entity attribute.</summary>
    public const string OrderIdField = "OrderId";
    /// <summary>Identifies the AdminId entity attribute.</summary>
    public const string AdminIdField = "AdminId";
    /// <summary>Identifies the PicTypeId entity attribute.</summary>
    public const string PicTypeIdField = "PicTypeId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("PicInfos")]
    private readonly EntityHolder<Admin> _admin = new EntityHolder<Admin>();
    [ReverseAssociation("PicInfos")]
    private readonly EntityHolder<PicType> _picType = new EntityHolder<PicType>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public Admin Admin
    {
      get { return Get(_admin); }
      set { Set(_admin, value); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public PicType PicType
    {
      get { return Get(_picType); }
      set { Set(_picType, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string Title
    {
      get { return Get(ref _title, "Title"); }
      set { Set(ref _title, value, "Title"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Link
    {
      get { return Get(ref _link, "Link"); }
      set { Set(ref _link, value, "Link"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Pic
    {
      get { return Get(ref _pic, "Pic"); }
      set { Set(ref _pic, value, "Pic"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime WriteTime
    {
      get { return Get(ref _writeTime, "WriteTime"); }
      set { Set(ref _writeTime, value, "WriteTime"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public int OrderId
    {
      get { return Get(ref _orderId, "OrderId"); }
      set { Set(ref _orderId, value, "OrderId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Admin" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int AdminId
    {
      get { return Get(ref _adminId, "AdminId"); }
      set { Set(ref _adminId, value, "AdminId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="PicType" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int PicTypeId
    {
      get { return Get(ref _picTypeId, "PicTypeId"); }
      set { Set(ref _picTypeId, value, "PicTypeId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class PicType : Entity<int>
  {
    #region Fields
  
    [ValidateLength(0, 50)]
    private string _picTypeName;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the PicTypeName entity attribute.</summary>
    public const string PicTypeNameField = "PicTypeName";


    #endregion
    
    #region Relationships

    [ReverseAssociation("PicType")]
    private readonly EntityCollection<PicInfo> _picInfos = new EntityCollection<PicInfo>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<PicInfo> PicInfos
    {
      get { return Get(_picInfos); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string PicTypeName
    {
      get { return Get(ref _picTypeName, "PicTypeName"); }
      set { Set(ref _picTypeName, value, "PicTypeName"); }
    }

    #endregion
  }




  /// <summary>
  /// Provides a strong-typed unit of work for working with the LightSpeedModel model.
  /// </summary>
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial class LightSpeedModelUnitOfWork : Mindscape.LightSpeed.UnitOfWork
  {

    public System.Linq.IQueryable<Admin> Admins
    {
      get { return this.Query<Admin>(); }
    }
    
    public System.Linq.IQueryable<NewsInfo> NewsInfos
    {
      get { return this.Query<NewsInfo>(); }
    }
    
    public System.Linq.IQueryable<NewsType> NewsTypes
    {
      get { return this.Query<NewsType>(); }
    }
    
    public System.Linq.IQueryable<PicInfo> PicInfos
    {
      get { return this.Query<PicInfo>(); }
    }
    
    public System.Linq.IQueryable<PicType> PicTypes
    {
      get { return this.Query<PicType>(); }
    }
    
  }

}
