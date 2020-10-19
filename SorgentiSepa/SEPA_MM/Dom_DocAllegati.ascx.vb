
Partial Class Dom_DocAllegati
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	    If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            CaricaLista()
            lblAggDocumenti.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "document.getElementById('H1').value='0';window.open('Locatari/AggiungiDocAlleg.aspx','AggDocumenti','top=200,left=500,width=580,height=400,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Aggiungi nuovo doc.allegato</a>"

            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                End If
            Next

        End If

    End Sub

    Public Sub CaricaLista()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            chkListDocumenti.Items.Clear()

            'par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari where id in (select id_documento from vsa_doc_tipo_necessari where id_tipo_domanda = " & CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbTipoRichiesta"), DropDownList).SelectedItem.Value & ")"
            par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari order by descrizione asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                Me.chkListDocumenti.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            '12/01/2012 Eliminazione ITEM doc al in base alla sottotipologia di richiesta

            'OSPITALITA per assistenza
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "15" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(25))
            'End If
            ''OSPITALITA per esigenze scolastiche
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "16" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(7))
            'End If

            ''AMPLIAMENTO Conv. More Uxorio
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "4" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(6))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(7))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(13))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(14))
            'End If

            ''AMPLIAMENTO Persone Legate da Vincolo di Affinità o Rientro
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "6" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "3" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(6))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(7))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(12))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(13))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(14))
            'End If

            ''AMPLIAMENTO Convivenza Ex Art 20 C.3 (R.R.1/04)
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "5" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(12))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(13))
            'End If

            ''SUBENTRO Decesso del Titolare
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "8" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "12" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "0" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(17))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(18))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(20))
            'End If

            ''SUBENTRO Separazione coniugi
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "9" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "13" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "1" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(17))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(18))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(19))
            'End If

            ''SUBENTRO Rinuncia del Titolare
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "10" Or CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "2" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(19))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(20))
            'End If

            ''SUBENTRO FF.OO. Fine Rapporto di Lavoro
            'If CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbPresentaD"), DropDownList).SelectedValue = "11" Then
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(17))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(19))
            '    chkListDocumenti.Items.Remove(chkListDocumenti.Items.FindByValue(20))
            'End If

            '12/01/2012 FINE Eliminazione ITEM in base alla sottotipologia di richiesta



            'For i As Integer = 0 To chkListDocumenti.Items.Count - 1
            '    chkListDocumenti.Items(i).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            'Next
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).lIdDichiarazione) Then
                par.cmd.CommandText = "select id_doc from vsa_doc_allegati where id_dichiarazione = " & CType(Me.Page, Object).lIdDichiarazione
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    documAlleg.Value = 1
                    Me.chkListDocumenti.Items.FindByValue(lettore("id_doc")).Selected = True
                End While
                lettore.Close()
            End If

            If CType(Me.Page.FindControl("btnSalva"), ImageButton).Visible = False Then
                DisattivaTutto()
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Public Sub DisattivaTutto()
        Dim i As Integer = 0
        For i = 0 To Me.chkListDocumenti.Items.Count - 1
            Me.chkListDocumenti.Items(i).Enabled = False
        Next
    End Sub
End Class
