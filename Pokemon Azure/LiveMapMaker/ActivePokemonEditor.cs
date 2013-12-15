using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokeEngine.Pokemon;
using Pokemon_Base_Stats_Editor;

namespace LiveMapMaker
{
    public partial class ActivePokemonEditor : Form
    {

        public ActivePokemon activePokemon { get; set; }

        public ActivePokemonEditor()
        {
            InitializeComponent();
        }

        private void btn_SaveExit_Click(object sender, EventArgs e)
        {

            activePokemon.baseStat = (BaseStat)(lbox_PokemonList.SelectedItem);

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivePokemonEditor_Load(object sender, EventArgs e)
        {
            foreach (BaseStat bs in BaseStatsList.basestats)
                lbox_PokemonList.Items.Add(bs);
        }

        private void lbox_PokemonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            clbox_MoveList.Items.Clear();
        }
    }
}
