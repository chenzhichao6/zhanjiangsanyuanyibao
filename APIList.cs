using System.Collections;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    class APIName {
        public string MethodName;
        public string Description;
        public string Body { get; set; }

        public APIName(string name, string desc)
        {
            this.MethodName = name;
            this.Description = desc;
        }

        public override string ToString()
        {
            return this.MethodName + " " + this.Description;
        }
    }

    class APIList
    {
        private List<KeyValuePair<string, APIName>> ApiList = new List<KeyValuePair<string, APIName>>();
        //private List<APIName> ApiLists = new List<APIName>();

        private APIName _addApi(string k, string desc)
        {
            APIName an = new APIName(k, desc);
            ApiList.Add(new KeyValuePair<string, APIName>(k, an));
            return an;
        }

        public void init()
        {
            //公共类
            _addApi("900100", "数字签名生成功能接口").Body = @"<METHOD>{0}</METHOD>
<ORGCODE>JKZ44081204</ORGCODE>
<USER>JKZ44081204</USER>
<PASSWORD>e10adc3949ba59abbe56e057f20f883e</PASSWORD>
<UUID>3a326dba086047df2090e6161e4949fc</UUID>";

            //业务登记类
            _addApi("C00300", "体检登记接口").Body = @"<VEHICLE>体检车辆 车牌号码</VEHICLE>
<ORDER></ORDER>
<ORDERTYPE></ORDERTYPE>
<NAME></NAME>
<IDCARD></IDCARD>
<GENDER></GENDER>
<AGE></AGE>
<ETHNIC></ETHNIC>
<HEAD></HEAD>
<CAPHEAD></CAPHEAD>
<FACE></FACE>
<MOBILE></MOBILE>
<OPENID></OPENID>
<TYPE></TYPE>
<INDUSTRY></INDUSTRY>
<DEGREE></DEGREE>
<ORDERTIME></ORDERTIME>
<ENROLLTIME></ENROLLTIME>
<ENROLLNO></ENROLLNO>";
            _addApi("C00400", "体检项目结果接口");
            _addApi("C00500", "健康证打印接口");
            _addApi("C00600", "短信通知上传接口");
            _addApi("C00700", "人脸比对数据接口");
            //业务回退类
            _addApi("C00800", "体检登记回退接口");
            _addApi("C00900", "体检结果体检明细回退接口");
            _addApi("C01100", "人脸比对数据回退接口");
            _addApi("C01300", "健康证打印回退接口");
            _addApi("C01400", "短信通知上传回退接口");
            //业务查询类
            _addApi("C00100", "查询体检机构信息接口");
            _addApi("C01500", "查询体检登记接口");
            _addApi("C01600", "查询体检项目接口");
            _addApi("C01700", "查询体检项目明细接口");
            _addApi("C01800", "查询人脸比对数据接口");
            _addApi("C02000", "查询健康证打印接口");
            _addApi("C02100", "查询短信通知接口");
        }

        public List<KeyValuePair<string, APIName>> getList()
        {
            return ApiList;
        }
    }
}
