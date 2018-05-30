using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using RaceAgainstSpace.Services;
using RaceAgainstSpace.Models;
using Firebase;
using Firebase.CloudFirestore;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(RaceAgainstSpace.iOS.Services.FirestoreService))]
namespace RaceAgainstSpace.iOS.Services
{
    public class FirestoreService : IFirestore<Card>
    {
        public static List<Card> cards;
        public FirestoreService()
        {
            cards = new List<Card>();
            Firestore dbRef = Firestore.Create(Firebase.Core.App.DefaultInstance);
            dbRef.GetCollection("cards").GetDocuments((snap, err)=>
            {
                if (err != null)
                    Debug.WriteLine(err);
                else
                {
                    DocumentSnapshot[] docs = snap.Documents;
                    foreach (DocumentSnapshot doc in docs) {
                        if (doc.Exists)
                        {
                            var data = (CardObj)doc.Data;
                            if (data != null)
                                cards.Add(data);
                            else
                            {
                                Card errorCard = new Card
                                {
                                    Text = doc.Id,
                                    Subtext = doc.Reference.ToString(),
                                    Type = "error"
                                };
                                cards.Add(errorCard);
                            }
                        }
                    }
                }
            });
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(cards);
        }

        public async Task<bool> AddCardAsync(Card card)
        {
            cards.Add(card);
            return await Task.FromResult<bool>(cards.Contains(card));
        }
    }

    class CardObj : Card
    {
        public static explicit operator CardObj(NSDictionary<NSString, NSObject> dictionary)
        {
            try
            {
                var retCard = new CardObj
                {
                    Text = (NSString)dictionary["text"].ToString(),
                    Subtext = (NSString)dictionary["subtext"].ToString(),
                    Type = (NSString)dictionary["type"].ToString()
                };
                return retCard;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine($"Error 1: {e}");
                return null;
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine($"Error2: {e}");
                return null;
            }
        }
    }
}