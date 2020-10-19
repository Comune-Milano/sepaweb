
Partial Class ASS_AnnullaAssegnazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sOF As String
    Dim scriptblock As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            HiddenField2.Value = ""
            sOF = Request.QueryString("OF")
            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"

            Cerca()


        End If
    End Sub

    Sub Cerca()
        Dim Ms_Stringa As String

        Dim m As Boolean

        m = False

        Ms_Stringa = ""

        If sOF <> "" Then
            Ms_Stringa = " AND unita_assegnate.n_offerta='" & par.PulisciStrSql(sOF) & "' "
            m = True
        End If


        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(UNITA_ASSEGNATE.DATA_assegnazione,'YYYYmmdd'),'DD/MM/YYYY') AS ""data_assegnazione"",UNITA_ASSEGNATE.n_offerta,unita_assegnate.cognome_rs,unita_assegnate.nomE,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,unita_assegnate.id_unita,unita_assegnate.cf_piva,UNITA_ASSEGNATE.ID_DOMANDA from SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.unita_assegnate where UNITA_IMMOBILIARI.ID=UNITA_ASSEGNATE.ID_UNITA AND generato_contratto='0' " & Ms_Stringa _
                       & " ORDER BY data_assegnazione desc"

        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(UNITA_ASSEGNATE.DATA_assegnazione,'YYYYmmdd'),'DD/MM/YYYY') AS data_assegnazione," _
            & "UNITA_ASSEGNATE.n_offerta,unita_assegnate.cognome_rs,unita_assegnate.nomE,unita_assegnate.id_unita," _
            & "unita_assegnate.cf_piva,UNITA_ASSEGNATE.ID_DOMANDA,(CASE WHEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE is not null THEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ELSE ALLOGGI.COD_ALLOGGIO END) AS COD_UNITA_IMMOBILIARE " _
            & "from SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.unita_assegnate, ALLOGGI where UNITA_ASSEGNATE.ID_UNITA=ALLOGGI.ID (+) AND UNITA_ASSEGNATE.ID_UNITA=UNITA_IMMOBILIARI.ID (+) AND generato_contratto='0' " & Ms_Stringa & " ORDER BY data_assegnazione desc"


        BindGrid()
    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "unita_assegnate")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub



    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        LBLID.Text = e.Item.Cells(0).Text
        HiddenField2.Value = e.Item.Cells(0).Text
        CodAlloggio.Value = e.Item.Cells(1).Text
        IdDomanda.Value = e.Item.Cells(2).Text
        idunita.Value = e.Item.Cells(3).Text
        cf_piva.Value = e.Item.Cells(4).Text
        Label2.Text = "Hai selezionato: Offerta " & e.Item.Cells(0).Text & " Alloggio:" & e.Item.Cells(1).Text
    End Sub



    Protected Sub btAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAnnulloAss.aspx""</script>")

    End Sub

    Protected Sub btnAccetta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccetta.Click
        If HiddenField1.Value = "1" Then
            If LBLID.Text = "-1" Or LBLID.Text = "" Then
                Response.Write("<script>alert('Nessuna Assegnazione selezionata!')</script>")
            Else
                Try
                    Dim relazione As String = ""
                    Dim ID_ALLOGGIO As String = ""
                    Dim MOTIVO As String = ""
                    Dim Annullo_Acc As Integer = 0

                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans



                    If RadioButton1.Checked = True Then
                        MOTIVO = RadioButton1.Text
                        Annullo_Acc = 0
                    End If

                    If RadioButton2.Checked = True Then
                        MOTIVO = RadioButton2.Text
                        Annullo_Acc = 1
                    End If

                    If RadioButton3.Checked = True Then
                        MOTIVO = RadioButton3.Text
                        Annullo_Acc = 2
                    End If

                    If RadioButton4.Checked = True Then
                        MOTIVO = RadioButton4.Text
                        Annullo_Acc = 3
                    End If

                    If RadioButton5.Checked = True Then
                        MOTIVO = RadioButton5.Text
                        Annullo_Acc = 4
                    End If

                    If RadioButton6.Checked = True Then
                        MOTIVO = RadioButton6.Text
                        Annullo_Acc = 5
                    End If

                    Dim UsiDiversi As Boolean = False


                    If MOTIVO <> "" Then

                        par.cmd.CommandText = "SELECT id from alloggi where cod_alloggio='" & CodAlloggio.Value & "'"
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.Read() Then
                            ID_ALLOGGIO = myReader("id")
                            UsiDiversi = False
                        End If
                        myReader.Close()

                        If ID_ALLOGGIO = "-1" Or ID_ALLOGGIO = "" Then
                            par.cmd.CommandText = "SELECT id from siscom_mi.UI_usi_diversi where cod_alloggio='" & CodAlloggio.Value & "'"
                            Dim myReaderx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderx.Read() Then
                                ID_ALLOGGIO = myReaderx("id")
                                UsiDiversi = True
                            End If
                            myReaderx.Close()
                        End If

                        If ID_ALLOGGIO = "-1" Or ID_ALLOGGIO = "" Then
                            ID_ALLOGGIO = "-1"
                        End If
                        If IdDomanda.Value <> "-1" Then
                            par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & IdDomanda.Value & " and ultimo='1'"
                            myReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                relazione = myReader("id")
                            Else
                                relazione = "1"
                            End If
                            myReader.Close()

                            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='4',MOTIVAZIONE='" & MOTIVO & "',ID_ANNULLO_ACC=" & Annullo_Acc & " WHERE ID=" & relazione
                            par.cmd.ExecuteNonQuery()

                            If IdDomanda.Value < 500000 Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & IdDomanda.Value
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & IdDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F104','','I')"
                                par.cmd.ExecuteNonQuery()
                            Else
                                If IdDomanda.Value > 8000000 Then
                                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & IdDomanda.Value
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & IdDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                    & "','F104','','I')"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_cambi SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & IdDomanda.Value
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_cambi (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & IdDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                    & "','F104','','I')"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If

                        End If
                        If UsiDiversi = False Then
                            par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',data_prenotato='' WHERE ID=" & ID_ALLOGGIO
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                            & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyymmdd") & "'," _
                            & "11,5," _
                            & ID_ALLOGGIO & "," _
                            & IdDomanda.Value & ",'" & MOTIVO & "')"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',data_prenotato='' WHERE ID=" & ID_ALLOGGIO
                            par.cmd.ExecuteNonQuery()
                        End If




                        par.cmd.CommandText = "delete from siscom_mi.unita_assegnate where id_domanda=" & IdDomanda.Value & " and generato_contratto=0 and cf_piva='" & cf_piva.Value & "' and id_unita=" & idunita.Value
                        par.cmd.ExecuteNonQuery()

                        LBLID.Text = ""
                        Label2.Text = "Nessuna Selezione"
                        par.myTrans.Commit()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Operazione Effettuata!');</script>")
                        BindGrid()
                    Else
                        par.myTrans.Commit()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Specificare il motivo!');</script>")
                    End If



                    Label2.Text = "Nessuna selezione"
                    BindGrid()
                    'End If

                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    If EX1.Number = 54 Then
                        par.myTrans.Rollback()
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        Label2.Text = EX1.Message
                        'scriptblock = "<script language='javascript' type='text/javascript'>" _
                        '& "alert('Annullo Assegnazione non possibile in questo momento!');" _
                        '& "</script>"
                        'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        'End If
                    Else
                        par.myTrans.Rollback()
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        Label2.Text = EX1.Message
                        'scriptblock = "<script language='javascript' type='text/javascript'>" _
                        '& "alert('" & EX1.ToString & "');" _
                        '& "</script>"
                        'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        'End If
                    End If
                Catch ex As Exception
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Label2.Text = ex.Message
                    'scriptblock = "<script language='javascript' type='text/javascript'>" _
                    '& "alert('" & ex.ToString & "');" _
                    '& "</script>"
                    'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    'End If
                End Try
            End If
        End If
    End Sub
End Class
