
Partial Class VSA_Locatari_AggiungiDocAlleg
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            Dim descrizioneDoc As String = ""
            Dim esiste As Boolean = False
            Dim strTabella As String = "<ul>"
            Dim docSimili As Boolean = False
            Dim docSelezionato As String = ""
            If txtDescrizione.Text = "" Then
                Response.Write("<script>alert('Attenzione...Inserire la descrizione del documento!')</script>")
                Exit Try
            End If
            descrizioneDoc = LCase(txtDescrizione.Text)

            If chkElencoDoc.Visible = True Then
                For i = 0 To chkElencoDoc.Items.Count - 1
                    If chkElencoDoc.Items(i).Selected = True Then
                        'img_InserisciSchema.Enabled = False
                        docSelezionato = "1"
                        Exit For
                    Else
                        img_InserisciSchema.Enabled = True
                        docSelezionato = "0"
                    End If
                Next
            End If

            If docSelezionato = "" Or docSelezionato = "0" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
            End If

            If docSelezionato = "1" Then
                Response.Write("<script>alert('Si prega di inserire un documento nuovo!!')</script>")
            End If
            
            'If docSelezionato = "" Then
            '    chkElencoDoc.Items.Clear()
            '    par.cmd.CommandText = "SELECT * FROM VSA_DOC_NECESSARI WHERE DESCRIZIONE LIKE '%" & primalettera(descrizioneDoc) & "%' OR DESCRIZIONE LIKE '%" & LCase(descrizioneDoc) & "%' OR DESCRIZIONE LIKE '%" & UCase(descrizioneDoc) & "%' ORDER BY DESCRIZIONE ASC"
            '    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReader0.Read
            '        docSimili = True
            '        chkElencoDoc.Visible = True
            '        chkElencoDoc.Items.Add(New ListItem(primalettera(par.IfNull(myReader0("descrizione"), "")), par.IfNull(myReader0("id"), -1)))
            '    End While
            '    myReader0.Close()

            '    If docSimili = True Then
            '        Response.Write("<script>alert('Attenzione...prima di procedere verificare se il documento è già presente!')</script>")
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        Exit Try
            '    End If
            'End If

            If docSelezionato = "0" Or (docSimili = False And docSelezionato = "") Then
                par.cmd.CommandText = "SELECT * FROM VSA_DOC_NECESSARI ORDER BY DESCRIZIONE ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    If LCase(Trim(par.IfNull(myReader("DESCRIZIONE"), ""))) = LCase(Trim(txtDescrizione.Text)) Then
                        esiste = True
                    End If
                End While
                myReader.Close()


                'Dim scriptblock As String = ""
                'scriptblock = "<script language='javascript' type='text/javascript'>" _
                '& "var chiediConferma;var msg1 = 'Attenzione, confermare l\'inserimento del documento?';chiediConferma = window.confirm(msg1);if (chiediConferma == true) {document.getElementById('conferma').value = '1';}else {document.getElementById('conferma').value = '0';}" _
                '& "</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript1")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript1", scriptblock)
                'End If
                Dim query As String = "SELECT * FROM VSA_DOC_NECESSARI WHERE DESCRIZIONE LIKE '%" & LCase(descrizioneDoc) & "%' OR DESCRIZIONE LIKE '%" & UCase(descrizioneDoc) & "%' OR DESCRIZIONE LIKE '%" & primalettera(descrizioneDoc) & "%' "

                Dim scriptblock As String = ""
                Dim TestArray() As String = Split(descrizioneDoc, " ")
                For i As Integer = 0 To TestArray.Length - 1
                    Dim rep As String = Replace(TestArray(i), ".", "")
                    If rep <> "" And Len(rep) > 2 And IsNumeric(rep) = False Then
                        query &= "OR DESCRIZIONE LIKE '%" & LCase(rep) & "%' OR DESCRIZIONE LIKE '%" & UCase(rep) & "%' "
                    End If
                Next

                Dim docSimil As Boolean = False
                Dim inserito As Boolean = False

                scriptblock = "Documenti simili: \n"
                par.cmd.CommandText = query & "ORDER BY DESCRIZIONE ASC"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader0.Read
                    docSimil = True
                    scriptblock &= "- " & Replace(par.IfNull(myReader0("descrizione"), ""), "'", "\'") & "\n"

                End While
                    myReader0.Close()
                scriptblock &= "\nAttenzione, confermare l\'inserimento del documento?"

                If esiste = False And TestArray.Length = 1 And Len(TestArray(0)) <= 2 Then

                    opSalva()
                    docSimil = False
                    Response.Write("<script language='javascript' type='text/javascript'>alert('Operazione effettuata!');</script>")
                    txtDescrizione.Text = ""
                    inserito = True

                ElseIf docSimil = True And esiste = False Then

                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript1")) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript1", "ConfermaSimili('" & scriptblock & "');", True)
                        inserito = True
                    End If
                End If
                If inserito = False Then
                    If esiste = False Then
                        par.cmd.CommandText = "INSERT INTO VSA_DOC_NECESSARI (ID,DESCRIZIONE) VALUES (SEQ_VSA_DOC_NECESSARI.NEXTVAL,'" & par.PulisciStrSql(UCase(descrizioneDoc)) & "')"
                        par.cmd.ExecuteNonQuery()
                        'img_InserisciSchema.Enabled = False
                        Response.Write("<script language='javascript' type='text/javascript'>alert('Operazione effettuata!');</script>")
                        txtDescrizione.Text = ""
                    Else
                        Response.Write("<script>alert('Documento già presente!');</script>")
                        txtDescrizione.Text = ""
                    End If
                    'End If
                End If
            End If

            If docSelezionato = "" Or docSelezionato = "0" Or conferma.Value = "0" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function primalettera(ByVal stringa As String) As String

        primalettera = UCase(Left(stringa, 1)) & LCase(Right(stringa, Len(stringa) - 1))

    End Function

    Protected Sub btnfunzSimili_Click(sender As Object, e As System.EventArgs) Handles btnfunzSimili.Click
        opSalva()

    End Sub

    Protected Sub opSalva()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "INSERT INTO VSA_DOC_NECESSARI (ID,DESCRIZIONE) VALUES (SEQ_VSA_DOC_NECESSARI.NEXTVAL,'" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "')"
            par.cmd.ExecuteNonQuery()
            'img_InserisciSchema.Enabled = False
            'Response.Write("<script language='javascript' type='text/javascript'>alert('Operazione effettuata!');</script>")
            'txtDescrizione.Text = ""

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

End Class
