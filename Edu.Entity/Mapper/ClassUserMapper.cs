/*******************************************************
*创建作者： Jerry
*类的名称： ClassUser
*命名空间： Edu.Entity.Mapper
*创建时间： 2020/3/24
********************************************************/
using Atom.EF.Base;
using System;
namespace Edu.Entity.Mapper
{
	 public class ClassUserMapper : BaseMap<ClassUser>
	 {
		public override void Init()
		{
			ToTable("ClassUser");
			HasKey(m => m.Id);
		}
	 }
}
