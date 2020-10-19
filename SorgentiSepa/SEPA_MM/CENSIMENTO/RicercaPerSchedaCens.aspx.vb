
Partial Class CENSIMENTO_RicercaPerSchedaCens
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            LoadDrlSchede()
        End If
    End Sub
    Private Sub LoadDrlSchede()
        Me.DrlSchede.Items.Add(New ListItem("- - - - - - - - - - - - - - - - - - ", -1))
        Me.DrlSchede.Items.Add(New ListItem("Sc. A-RILIEVO STRUTTURE", "A"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. B-SCHEDA RILIEVO CHIUSURE", "B"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. C-SCHEDA RILIEVO PARTIZIONI INTERNE", "C"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. D-SCHEDA RILIEVO PAVIMENTAZIONI INTERNE", "D"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. E-SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI", "E"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. F-SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI", "F"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. G-SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI", "G"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. H-SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO", "H"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. I-SCHEDA RILIEVO IMPIANTI  RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA", "I"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. L-SCHEDA RILIEVO IMPIANTI IDRICO SANITARI", "L"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. M-SCHEDA RILIEVO IMPIANTI ANTINCENDIO", "M"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. N-SCHEDA RILIEVO RETE SCARICO / FOGNARIA", "N"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. O-SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI ", "O"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. P-SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS ", "P"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. Q-SCHEDA RILIEVO IMPIANTI ELETTRICI", "Q"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. R-SCHEDA RILIEVO IMPIANTI TELEVISIVI", "R"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. S-SCHEDA RILIEVO IMPIANTI CITOFONI", "S"))
        Me.DrlSchede.Items.Add(New ListItem("Sc. T-SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE", "T"))

    End Sub

End Class
