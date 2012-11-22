using System;
using System.Linq;
using FluentNHibernate.Automapping;
using MrCMS.Entities;
using MrCMS.Entities.Documents;

namespace MrCMS.DbConfiguration.Mapping
{
    public class TheventsMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof (BaseEntity)) &&
                   (type.Assembly == typeof (BaseEntity).Assembly || HasMappingAttribute(type)) && base.ShouldMap(type) &&
                   !HasDoNotMapAttribute(type);
        }

        private bool HasDoNotMapAttribute(Type type)
        {
            return type.GetCustomAttributes(typeof(DoNotMapAttribute), false).Any();
        }

        private bool HasMappingAttribute(Type type)
        {
            return type.GetCustomAttributes(typeof(MrCMSMapClassAttribute), false).Any();
        }

        public override bool ShouldMap(FluentNHibernate.Member member)
        {
            return member.CanWrite && base.ShouldMap(member);
        }

        public override bool IsId(FluentNHibernate.Member member)
        {
            return member.Name == "Id" && base.IsId(member);
        }

        public override bool IsDiscriminated(System.Type type)
        {
            return type.IsSubclassOf(typeof(Document));
        }
    }
}