using System.Runtime.Serialization;

namespace Mook.DapperCore
{
    /// <summary>
    /// 分页属性
    /// </summary>
    public class PageParam
    {
        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置每页显示的记录
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 获取或设置记录总数
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        [DataMember]
        public string SortCondition { get; set; }
    }
}
