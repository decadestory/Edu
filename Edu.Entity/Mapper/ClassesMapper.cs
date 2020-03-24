/*******************************************************
*创建作者： Jerry
*类的名称： Classes
*命名空间： Edu.Entity.Mapper
*创建时间： 2020/3/24
********************************************************/
using Atom.EF.Base;
using System;
namespace Edu.Entity.Mapper
{
	 public class ClassesMapper : BaseMap<Classes>
	 {
		public override void Init()
		{
			ToTable("Classes");
			HasKey(m => m.Id);
		}
	 }
}
