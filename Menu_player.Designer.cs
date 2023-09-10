
namespace Проект
{
    partial class Menu_player
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hello = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.list_player = new System.Windows.Forms.Button();
            this.list_protocol = new System.Windows.Forms.Button();
            this.name_competition = new System.Windows.Forms.Label();
            this.player_view = new System.Windows.Forms.DataGridView();
            this.prot_view = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prot_view)).BeginInit();
            this.SuspendLayout();
            // 
            // hello
            // 
            this.hello.AutoSize = true;
            this.hello.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hello.Location = new System.Drawing.Point(197, 57);
            this.hello.Name = "hello";
            this.hello.Size = new System.Drawing.Size(53, 20);
            this.hello.TabIndex = 11;
            this.hello.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(197, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "ТЕХНИЧЕСКАЯ ДИРЕКЦИЯ";
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.DodgerBlue;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exit.Location = new System.Drawing.Point(27, 24);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(143, 68);
            this.exit.TabIndex = 9;
            this.exit.Text = "Выход";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Проект.Properties.Resources.Logowsr;
            this.pictureBox1.Location = new System.Drawing.Point(1244, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // list_player
            // 
            this.list_player.BackColor = System.Drawing.Color.DodgerBlue;
            this.list_player.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.list_player.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.list_player.Location = new System.Drawing.Point(520, 198);
            this.list_player.Name = "list_player";
            this.list_player.Size = new System.Drawing.Size(381, 67);
            this.list_player.TabIndex = 12;
            this.list_player.Text = "Список участников";
            this.list_player.UseVisualStyleBackColor = false;
            this.list_player.Click += new System.EventHandler(this.list_player_Click);
            // 
            // list_protocol
            // 
            this.list_protocol.BackColor = System.Drawing.Color.DodgerBlue;
            this.list_protocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.list_protocol.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.list_protocol.Location = new System.Drawing.Point(520, 310);
            this.list_protocol.Name = "list_protocol";
            this.list_protocol.Size = new System.Drawing.Size(381, 67);
            this.list_protocol.TabIndex = 13;
            this.list_protocol.Text = "Список протоколов";
            this.list_protocol.UseVisualStyleBackColor = false;
            this.list_protocol.Click += new System.EventHandler(this.list_protocol_Click);
            // 
            // name_competition
            // 
            this.name_competition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_competition.Location = new System.Drawing.Point(689, 24);
            this.name_competition.Name = "name_competition";
            this.name_competition.Size = new System.Drawing.Size(522, 68);
            this.name_competition.TabIndex = 21;
            this.name_competition.Text = "Чемпионат";
            // 
            // player_view
            // 
            this.player_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.player_view.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.player_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.player_view.Location = new System.Drawing.Point(124, 128);
            this.player_view.Name = "player_view";
            this.player_view.RowHeadersWidth = 51;
            this.player_view.RowTemplate.Height = 24;
            this.player_view.Size = new System.Drawing.Size(1182, 405);
            this.player_view.TabIndex = 22;
            // 
            // prot_view
            // 
            this.prot_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.prot_view.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.prot_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prot_view.Location = new System.Drawing.Point(124, 128);
            this.prot_view.Name = "prot_view";
            this.prot_view.RowHeadersWidth = 51;
            this.prot_view.RowTemplate.Height = 24;
            this.prot_view.Size = new System.Drawing.Size(1182, 405);
            this.prot_view.TabIndex = 23;
            this.prot_view.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.prot_view_RowHeaderMouseClick);
            // 
            // Menu_player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 553);
            this.Controls.Add(this.prot_view);
            this.Controls.Add(this.player_view);
            this.Controls.Add(this.name_competition);
            this.Controls.Add(this.list_protocol);
            this.Controls.Add(this.list_player);
            this.Controls.Add(this.hello);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Menu_player";
            this.Text = "Окно участника";
            this.Load += new System.EventHandler(this.Menu_player_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prot_view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hello;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button list_player;
        private System.Windows.Forms.Button list_protocol;
        private System.Windows.Forms.Label name_competition;
        private System.Windows.Forms.DataGridView player_view;
        private System.Windows.Forms.DataGridView prot_view;
    }
}