namespace TesteS2IT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Lended")]
    public partial class Lending
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Friend")]
        public int FriendId { get; set; }

        [ForeignKey("Games")]
        public int GameId { get; set; }

        [Display(Name = "Data Empréstimo")]
        public DateTime? DateLended { get; set; }

        [Display(Name = "Data Devolução")]
        public DateTime? DateReturned { get; set; }

        [Display(Name = "Amigo")]
        public virtual Friend Friend { get; set; }

        [Display(Name = "Jogo")]
        public virtual Games Games { get; set; }


        public static Lending FindLendingByFriend(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p.FriendId == id);

                }
                catch (Exception ex)
                {
                    ////LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Lending FindLendingByGame(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p.GameId == id);

                }
                catch (Exception ex)
                {
                    ////LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Lending FindLendingByFriend(string nameFriend)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p.Friend.Name.Equals(nameFriend));

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Lending FindLendingByGame(string nameFriend)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p.Games.Name.Equals(nameFriend));

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Lending FindLending(Lending lending)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p == lending);

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Lending FindLending(int lendingId)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").FirstOrDefault(p => p.ID == lendingId);

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Lending> FindLendingOpened()
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").Where(p => String.IsNullOrEmpty(p.DateReturned.ToString())).ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Lending> FindAllLending()
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Lending.Include("Friend").Include("Games").ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertLending(Lending lending)
        {
            using (var db = new DBContext())
            {
                try
                {
                    db.Lending.Add(lending);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertLending(List<Lending> Lending)
        {
            foreach (var item in Lending)
            {
                InsertLending(item);
            }

        }

        public static Lending UpdateLendingItem(Lending lending)
        {
            using (var db = new DBContext())
            {
                try
                {
                   
                        db.Lending.Attach(lending);
                        db.Entry(lending).State = EntityState.Modified;
                        db.SaveChanges();
                        return lending;
               

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new //LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Lending> UpdateLending(List<Lending> listLending)
        {
            List<Lending> returnList = new List<Lending>();
            foreach (var item in listLending)
            {
                returnList.Add(UpdateLendingItem(item));
            }
            return returnList;
        }

        public static Lending ExcludeLending(Lending lending)
        {
            using (var db = new DBContext())
            {
                db.Lending.Remove(lending);
                db.SaveChanges();
                return lending;
            }
        }

        public static Lending ExcludeLending(int id)
        {
            Lending returnLending = new Lending();

            returnLending = FindLending(id);

            ExcludeLending(returnLending);

            return returnLending;
        }
    }
}

