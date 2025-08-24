using Hachiko.Domain.Models;

namespace Hachiko.Domain.Interfaces;

interface IAmineService
{
    public Task<ICollection<Anime>> SearchAnime(string name);
}