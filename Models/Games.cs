namespace TesteS2IT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Games
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Tipo de Jogo")]
        [StringLength(50)]
        public string Type { get; set; }

        [Display(Name = "Ano")]
        [StringLength(4)]
        public string Year { get; set; }

        [Display(Name = "Link da Imagem")]
        [StringLength(50)]
        public string UrlImage { get; set; }

        public static Games FindGame(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Games.FirstOrDefault(p => p.ID == id);

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Games FindGame(string name)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Games.FirstOrDefault(p => p.Name.Equals(name));

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Games> FindGames(string name)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Games.Where(p => p.Name.Contains(name)).ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Games> FindNoLendedGames()
        {
            using (var db = new DBContext())
            {
                try
                {

                    //List<Games> gamesList = db.Games.ToList();
                    //List<Games> gamesOut = new List<Games>();
                    //List<Games> returnList = new List<Games>();

                    //foreach (var item in gamesList)
                    //{
                    //    var a = lendingList.Where(p => p.GameId == item.ID).ToList();
                    //    if (lendingList.Contains(item))
                    //    {

                    //    }
                    //    returnList.Add();
                    //}

                    //foreach (var item in lendingList)
                    //{
                    //    gamesOut.Add(item.Games);
                    //}
                    //returnList = gamesList.Except(gamesOut).ToList();

                    //foreach (var item in lendingList)
                    //{

                    //    gamesOut.Add(item.Games);

                    //}
                    //foreach (var item in gamesList)
                    //{
                    //    if (!gamesOut.Contains(item))
                    //    {
                    //        returnList.Add(item);
                    //    }
                    //}

                    //return returnList;
                    var lendingList = Lending.FindLendingOpened().AsQueryable();
                    //var listGames = db.Games.Where(g => !lendingList.Any(l => l.GameId == g.ID)).ToList();

                    var listGames = db.Games
                        .Where(g => !db.Lending.Any(l => l.GameId == g.ID && string.IsNullOrEmpty(l.DateReturned.Value.ToString())))
                        .ToList();

                    return listGames;
                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Games> FindAllGames()
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Games.ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertGame(Games game)
        {
            using (var db = new DBContext())
            {
                try
                {
                    db.Games.Add(game);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertGames(List<Games> game)
        {
            foreach (var item in game)
            {
                InsertGame(item);
            }

        }

        public static Games UpdateGames(Games game)
        {
            using (var db = new DBContext())
            {
                try
                {

                        db.Games.Attach(game);
                        db.Entry(game).State = EntityState.Modified;
                        db.SaveChanges();
                        return game;                   

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Games> UpdateGames(List<Games> listGames)
        {
            List<Games> returnList = new List<Games>();
            foreach (var item in listGames)
            {
                returnList.Add(UpdateGames(item));
            }
            return returnList;
        }

        public static Games ExcludeGame(Games game)
        {
            using (var db = new DBContext())
            {
                db.Games.Attach(game);
                db.Entry(game).State = EntityState.Deleted;
                db.SaveChanges();
                return game;
            }
        }

        public static Games ExcludeGame(int id)
        {
            Games returnGame = new Games();

            returnGame = FindGame(id);

            ExcludeGame(returnGame);

            return returnGame;
        }
    }
}