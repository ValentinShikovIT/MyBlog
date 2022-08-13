using AutoMapper;

namespace MyBlog.Common.Mapping
{
    public interface IMapExplicitly
    {
        public void RegisterMappings(IProfileExpression profile);
    }
}
