using System;
using System.Collections.Generic;
using gymMe.Model;
using SQLite;
using Xamarin.Forms;

namespace gymMe
{
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
        }

        private void UpdateButton_Clicked(object sender, System.EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Update(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully updated", "OK");
                else
                    DisplayAlert("Failure", "Experience failed to update", "OK");

            }

        }

        private void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Delete(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully deleted", "OK");
                else
                    DisplayAlert("Failure", "Experience failed to deleted", "OK");

            }

        }

    }
}
