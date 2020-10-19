Imports Telerik.Web.UI

'*** RICERCA MANUTENZIONI RRS fase pre INSERIMENTO 

Partial Class RRS_RicercaRRS_INS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim FlagConnessione As Boolean

        Try


            If Not IsPostBack Then

                ' APRO CONNESSIONE
                FlagConnessione = False
                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                '    par.SettaCommand(par)

                '    FlagConnessione = True
                'End If

                Tabber1 = "tabbertabdefault"

                Me.txtTIPO.Value = Request.QueryString("TIPOR")

                'ID ESERCIZIO FINANZIARIO
                'par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


                'If myReader1.Read Then
                '    Me.txtIdPianoFinanziario.Value = par.IfNull(myReader1("ID"), -1)
                'End If
                'myReader1.Close()
                ''***********************************

                'If FlagConnessione = True Then
                '    par.cmd.Dispose()
                '    par.OracleConn.Close()
                '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'End If

                CType(Tab_Ricerca2.FindControl("RBL1"), RadioButtonList).SelectedValue = 0

                CaricaIndirizzi()


            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub




    'CARICO COMBO INDIRIZZI del TAB. RICERCA 2
    Private Sub CaricaIndirizzi()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).Items.Clear()
            ' CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select MAX(ID) AS ID ,trim(DESCRIZIONE) as DESCRIZIONE  from SISCOM_MI.INDIRIZZI " _
                               & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI )" _
                                                & "   and ID>1)" _
                               & " GROUP BY DESCRIZIONE order by DESCRIZIONE asc"


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox), "ID", "DESCRIZIONE", True)
            CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Try

            ControlloCampi = True

            'If CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then
            '    ControlloCampi = False
            '    RadWindowManager1.RadAlert("Selezionare l\'indirizzo!", 300, 150, "Attenzione", "", "null")
            '    Exit Sub
            'End If

            If CType(Tab_Ricerca2.FindControl("cmbVoce"), RadComboBox).SelectedValue = "-1" Then
                ControlloCampi = False
                RadWindowManager1.RadAlert("Selezionare la Voce!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).SelectedValue = "-1" Then
                ControlloCampi = False
                RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If


            If ControlloCampi = True Then

                If CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then

                    Session.Remove("ID")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "location.replace('ManutenzioniRRS.aspx?ED=-1" _
                                                                                & "&CO=-1" _
                                                                                & "&AP=" & CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                                & "&SV=" & CType(Tab_Ricerca2.FindControl("cmbVoce"), RadComboBox).SelectedValue.ToString _
                                                                                & "&IN_R=" & par.VaroleDaPassare(CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedItem.Text) _
                                                                                & "&SV_R=" & CType(Tab_Ricerca2.FindControl("cmbVoce"), RadComboBox).SelectedValue.ToString _
                                                                                & "&AP_R=" & CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                                & "&UBI=" & CType(Tab_Ricerca2.FindControl("RBL1"), RadioButtonList).SelectedIndex _
                                                                                & "&EF_R=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue.ToString _
                                                                                & "&PROVENIENZA=RICERCARRS_INS');", True)


                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "location.replace('RisultatiRRS_INS.aspx?IN_R=" & par.VaroleDaPassare(CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedItem.Text) _
                                                                                                & "&SV_R=" & CType(Tab_Ricerca2.FindControl("cmbVoce"), RadComboBox).SelectedValue.ToString _
                                                                                                & "&AP_R=" & CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                                                & "&UBI=" & CType(Tab_Ricerca2.FindControl("RBL1"), RadioButtonList).SelectedIndex _
                                                                                                & "&EF_R=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue.ToString _
                                                                                                & "&PROVENIENZA=RISULTATI_RRS_INS" & "');", True)

                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
        End Select

    End Sub
End Class
