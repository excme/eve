using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Identity;
using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;

namespace Identity.Jobs.Tests
{
    public class List : UnitTestCore
    {
        public List(ITestOutputHelper output) : base(output)
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
        }

        [Fact]
        public void SsoTokeCheck()
        {
            var job = new SsoTokenCheck(_repoPublicCommon, null, clientId, secretKey);
            var sso = new IdentitySso() { refresh_token = "yY8lTrI1sSQT5LLPhXGJWg5y57H6TAfbxOhnBMdvc61Y90OtXG8Mer4-yIgzJjrb7gfkwfi37fg9FkPsnI50jLEaJj5wXlobDi9B1lqrga2Mdf-WoMzZrJ7jy1DdQN73dJmlkXJIyW3QzQgF0fYdv-8iAmPkHybMCAogmhcxjTNp9WZmtZldy-ibCTg9y8Mna5aLkYF94C8vEeInRuOllmufoPlKNY9AVrGCP3E_fZrjYrKMNoN9qHKys7meInsskGNPkqo-Te3i2x2DV3lihhVzUr5FqgSM5lvTCphiFq5_b47jOmhsg_bzUl67tYRfDYIeZe2c0aGwuXV4wSO_fvieeHZvUzJIjXdH1iMU-NlrOjhF2L8blKihrdyveDqfP_7Z90L3G02NfxB70mNIlw" };
            //await job.Execute();
            job.Simple(sso);
        }
    }
}
