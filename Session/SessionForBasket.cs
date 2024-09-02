using Newtonsoft.Json;

namespace BiletPortal.Session
{
    public static class SessionForBasket
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }

        public static T? GetJson <T> (this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
