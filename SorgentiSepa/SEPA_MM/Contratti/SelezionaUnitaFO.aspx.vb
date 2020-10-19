
Partial Class Contratti_SelezionaUnitaFO
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaSQL1 As String = ""

            sStringaSQL1 = "SELECT DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM siscom_mi.unita_immobiliari,T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE unita_immobiliari.id_destinazione_uso=6 and unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio and TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND (EQCANONE='0' OR FL_POR='1') AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            'par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, PAR.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "ALLOGGI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label9.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "popupWindow=window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If

    End Sub

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.Text = e.Item.Cells(1).Text
    '    Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text

    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''};" _
                                  & "Selezionato=this;this.style.backgroundColor='red';" _
                                  & "document.getElementById('Label9').value='Hai selezionato Unità Cod.: " & e.Item.Cells(PAR.IndDGC(DataGrid1, "COD_ALLOGGIO")).Text.ToString.Replace("'", "\'") & "';" _
                                  & "document.getElementById('LBLID').value='" & e.Item.Cells(PAR.IndDGC(DataGrid1, "COD_ALLOGGIO")).Text.ToString.Replace("'", "\'") & "';")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        'Response.Write("<script>alert('Non Disponibile!');</script>")
        Dim BUONO As Boolean = True

        If LBLID.Value <> "" Then
            Try
                Dim Codice As Long

                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)

                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & LBLID.Value & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.Read = True Then
                    Codice = PAR.IfNull(myReader("ID"), -1)


                    PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA=" & Codice & " ORDER BY ID_CONTRATTO DESC"
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReaderX.HasRows = True Then
                        If myReaderX.Read Then
                            PAR.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & myReaderX("ID_CONTRATTO")
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                            If myReader1.Read Then
                                If PAR.IfNull(myReader1("STATO"), "") <> "CHIUSO" Then

                                    BUONO = False


                                End If

                            End If
                            myReader1.Close()
                        End If
                    End If
                    'Loop
                    myReaderX.Close()

                    If BUONO = False Then

                        Response.Write("<script>alert('Unità non disponibile perchè occupata!');</script>")
                        myReader.Close()
                        PAR.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                    Else
                        myReader.Close()
                        PAR.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Redirect("DichiarazioneFO.aspx?U=" & Codice & "&CODICE=" & LBLID.Value)
                    End If




                End If

                'Response.Write("<script>A=window.open('Contratto.aspx?ID=-1&IdDichiarazione=1&IdDomanda=-1&IdUnita=" & CODICE & "&TIPO=3','Contratto" & Format(Now, "hhss") & "','height=680,width=900');A.focus();</script>")


            Catch ex As Exception
                PAR.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Seleziona Unita contratti Usi diversi - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        Else
            Response.Write("<script>alert('Selezionare una unità dalla lista!');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla0.Click
        Response.Redirect("SelezionaTipoContratto.aspx")
    End Sub
End Class
