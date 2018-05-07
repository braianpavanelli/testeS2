namespace TesteS2IT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Friend")]
    public partial class Friend
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Mail { get; set; }


        public static Friend FindFriend(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Friend.FirstOrDefault(p => p.ID == id);

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static Friend FindFriend(string name)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Friend.FirstOrDefault(p => p.Name.Equals(name));

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Friend> FindFriends(string name)
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Friend.Where(p => p.Name.Contains(name)).ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Friend> FindAllFriends()
        {
            using (var db = new DBContext())
            {
                try
                {
                    return db.Friend.ToList();

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Friend> FindAllFriendsWithLending()
        {
            using (var db = new DBContext())
            {
                try
                {
                    List<Lending> lendingList = Lending.FindLendingOpened();
                    List<Friend> friendList = db.Friend.ToList();
                    List<Friend> returnList = new List<Friend>();

                    foreach (var item in friendList)
                    {
                        if (lendingList.Any(p => p.FriendId == item.ID))
                        {
                            returnList.Add(item);
                        } 
                    }

                    return returnList;

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertFriend(Friend Friend)
        {
            using (var db = new DBContext())
            {
                try
                {
                    db.Friend.Add(Friend);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static void InsertFriend(List<Friend> Friends)
        {
            foreach (var item in Friends)
            {
                InsertFriend(item);
            }

        }

        public static Friend UpdateFriend(Friend friend)
        {
            using (var db = new DBContext())
            {
                try
                {

                    db.Friend.Attach(friend);
                    db.Entry(friend).State = EntityState.Modified;
                    db.SaveChanges();
                    return friend;

                }
                catch (Exception ex)
                {
                    //LogError.insertLog(new LogError(ex.Message, ex.StackTrace));
                    throw;
                }
            }
        }

        public static List<Friend> UpdateFriend(List<Friend> listFriend)
        {
            List<Friend> returnList = new List<Friend>();
            foreach (var item in listFriend)
            {
                returnList.Add(UpdateFriend(item));
            }
            return returnList;
        }

        public static Friend ExcludeFriend(Friend friend)
        {
            using (var db = new DBContext())
            {
                db.Friend.Remove(friend);
                db.SaveChanges();
                return friend;
            }
        }

        public static Friend ExcludeFriend(int id)
        {
            Friend returnFriend = new Friend();

            returnFriend = FindFriend(id);

            ExcludeFriend(returnFriend);

            return returnFriend;
        }
    }
}
