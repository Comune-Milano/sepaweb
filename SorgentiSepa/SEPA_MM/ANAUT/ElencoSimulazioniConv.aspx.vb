
Partial Class ANAUT_ElencoSimulazioniConv
    Inherits PageSetIdMode
    Dim par As New CM.Global

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

    Public Property IndiceBando() As Long
        Get
            If Not (ViewState("par_IndiceBando") Is Nothing) Then
                Return CLng(ViewState("par_IndiceBando"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceBando") = value
        End Set
    End Property

    Private Sub BindGrid()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT max(id) FROM UTENZA_BANDI WHERE stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IndiceBando = myReader(0)
            End If
            myReader.Close()


            'Dim Str As String = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DettagliSimulazione.aspx?IDS='||CONVOCAZIONI_AU_GRUPPI.ID||''',''Dettagli'','''');£>'||'DETTAGLI'||'</a>','$','&'),'£','" & Chr(34) & "') as VISUALIZZA,TO_CHAR(TO_DATE(INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS INIZIO,TO_CHAR(TO_DATE(FINE,'yyyymmdd'),'dd/mm/yyyy') AS FINE,CONVOCAZIONI_AU_GRUPPI.*,UTENZA_BANDI.DESCRIZIONE AS NOME FROM UTENZA_BANDI,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE UTENZA_BANDI.ID=CONVOCAZIONI_AU_GRUPPI.ID_AU AND SUBSTR(CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE,1,11)='SIMULAZIONE' ORDER BY CONVOCAZIONI_AU_GRUPPI.ID DESC, CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE ASC"
            Dim Str As String = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DettagliSimulazione.aspx?IDS='||CONVOCAZIONI_AU_GRUPPI.ID||''',''Dettagli'','''');£>'||'DETTAGLI'||'</a>','$','&'),'£','" & Chr(34) & "') as VISUALIZZA,TO_CHAR(TO_DATE(INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS INIZIO,TO_CHAR(TO_DATE(FINE,'yyyymmdd'),'dd/mm/yyyy') AS FINE,CONVOCAZIONI_AU_GRUPPI.*,UTENZA_BANDI.DESCRIZIONE AS NOME FROM UTENZA_BANDI,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE UTENZA_BANDI.ID=CONVOCAZIONI_AU_GRUPPI.ID_AU AND CONVOCAZIONI_AU_GRUPPI.FL_CONFERMATA=0 ORDER BY CONVOCAZIONI_AU_GRUPPI.ID DESC, CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "CONVOCAZIONI_AU_GRUPPI")

            DataGridCapitoli.DataSource = ds
            DataGridCapitoli.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub DataGridCapitoli_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCapitoli.ItemDataBound
        If e.Item.Cells(1).Text <> "DESCRIZIONE" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGridCapitoli_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCapitoli.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridCapitoli.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridCapitoli_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridCapitoli.SelectedIndexChanged

    End Sub

    Protected Sub ImgBtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnElimina.Click
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "delete from SISCOM_MI.AGENDA_APPUNTAMENTI where ID_CONVOCAZIONE IN (SELECT ID from SISCOM_MI.CONVOCAZIONI_AU where ID_GRUPPO = " & txtid.Value & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from SISCOM_MI.CONVOCAZIONI_AU where ID_GRUPPO = " & txtid.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from SISCOM_MI.CONVOCAZIONI_AU_GRUPPI where ID = " & txtid.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from SISCOM_MI.AGENDA_PARAMETRI_OP where ID_PROCEDIMENTO = " & txtid.Value
                par.cmd.ExecuteNonQuery()
                '


                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');</script>")
                txtid.Value = ""
                eliminato.Value = "0"

                BindGrid()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Protected Sub imgConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConferma.Click
        If confermato.value = "1" Then
            Try
                lblErrore.Visible = False

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                Dim MIADATA As String = Format(Now, "yyyyMMddHHmm")
                Dim buono As Boolean = True


                par.cmd.CommandText = "SELECT * FROM siscom_mi.CONVOCAZIONI_AU_GRUPPI where id=" & txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    'par.cmd.CommandText = "SELECT * FROM siscom_mi.CONVOCAZIONI_AU_GRUPPI WHERE  inizio is not null and fl_confermata=1 and id<>" & txtid.Value & " and INIZIO>'" & myReader("inizio") & "' AND INIZIO<'" & myReader("FINE") & "' AND ID IN (SELECT DISTINCT (id_gruppo) FROM siscom_mi.CONVOCAZIONI_AU WHERE data_app>'" & myReader("INIZIO") & "' AND data_app<'" & myReader("FINE") & "' AND id_contratto is not null and id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & ")) UNION SELECT * FROM siscom_mi.CONVOCAZIONI_AU_GRUPPI WHERE inizio is not null and fl_confermata=1 and id<>" & txtid.Value & " and FINE>'" & myReader("inizio") & "' AND FINE<'" & myReader("FINE") & "' AND ID IN (SELECT DISTINCT (id_gruppo) FROM siscom_mi.CONVOCAZIONI_AU WHERE data_app>'" & myReader("INIZIO") & "' AND data_app<'" & myReader("FINE") & "' AND id_contratto is not null and id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & "))"
                    'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader1.HasRows = True Then
                    '    buono = False
                    'Else
                    par.cmd.CommandText = "INSERT INTO siscom_mi.CONVOCAZIONI_AU_ELIMINATE (SELECT * FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo<>" & txtid.Value & " and id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & ") AND ID_CONTRATTO IS NULL AND data_app>='" & myReader("INIZIO") & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo<>" & txtid.Value & " and id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & ") AND ID_CONTRATTO IS NULL AND data_app>='" & myReader("INIZIO") & "'"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO siscom_mi.AGENDA_APPUNTAMENTI_ELIMINATI (SELECT * FROM siscom_mi.AGENDA_APPUNTAMENTI WHERE inizio>='" & myReader("inizio") & "0000' AND id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & ") AND ID_CONTRATTO is null and id_convocazione IS NOT NULL AND id_convocazione NOT IN (SELECT ID FROM siscom_mi.CONVOCAZIONI_AU WHERE id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & "))) "
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "DELETE FROM siscom_mi.AGENDA_APPUNTAMENTI WHERE inizio>='" & myReader("inizio") & "0000' AND id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & ") AND ID_CONTRATTO is null and id_convocazione IS NOT NULL AND id_convocazione NOT IN (SELECT ID FROM siscom_mi.CONVOCAZIONI_AU WHERE id_sportello IN (SELECT DISTINCT(id_sportello) FROM siscom_mi.CONVOCAZIONI_AU WHERE id_gruppo=" & txtid.Value & "))"
                    par.cmd.ExecuteNonQuery()
                    'End If
                    'myReader1.Close()
                End If
                myReader.Close()

                If buono = True Then

                    par.cmd.CommandText = "update siscom_mi.CONVOCAZIONI_AU_GRUPPI set fl_confermata=1,ID_OPERATORE_CONFERMA=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & txtid.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT rapporti_utenza.cod_contratto,CONVOCAZIONI_AU_GRUPPI.ID_AU,ANAGRAFICA.COGNOME AS CANAGRAFICA,ANAGRAFICA.NOME AS NANAGRAFICA,CONVOCAZIONI_AU.* FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND CONVOCAZIONI_AU.data_app IS NOT NULL AND CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU.ID_GRUPPO AND CONVOCAZIONI_AU.ID_GRUPPO=" & txtid.Value & " AND CONVOCAZIONI_AU.ID_CONTRATTO NOT IN (SELECT NVL(ID_CONTRATTO,0) FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_AU=CONVOCAZIONI_AU_GRUPPI.ID_AU) order by id_sportello asc,data_app asc,to_number(replace(ore_app,'.','')) asc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader("ID") & ",'" & MIADATA & "'," & Session.Item("ID_OPERATORE") & ",'CARICATA CONVOCAZIONE AU PER " & par.FormattaData(myReader("DATA_APP")) & " ORE " & myReader("ORE_APP") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_SPORTELLO=" & par.IfNull(myReader("ID_SPORTELLO"), "NULL") & " AND ID_CONTRATTO IS NULL AND ID_FILIALE=" & myReader("ID_FILIALE") & " AND ID_AU=" & myReader("ID_AU") & " AND INIZIO='" & myReader("DATA_APP") & Format(CInt(Replace(myReader("ORE_APP"), ".", "")), "0000") & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "Insert into SISCOM_MI.AGENDA_APPUNTAMENTI  (ID, ID_STATO, ID_FILIALE, ID_CONVOCAZIONE, ID_AU, N_OPERATORE, COD_CONTRATTO, COGNOME, NOME, " _
                            & "INIZIO, FINE, MOTIVAZIONE_ANNULLO, ID_CONTRATTO, TIPO_F_ORARIA, ID_SPORTELLO) Values " _
                            & "(SISCOM_MI.SEQ_AGENDA_APPUNTAMENTI.nextval, 0, " & myReader("ID_FILIALE") & ", " & par.IfNull(myReader("ID"), "NULL") & ", " & myReader("ID_AU") & ", '" & myReader("N_OPERATORE") & "', '" _
                            & par.IfNull(myReader("COD_CONTRATTO"), "") & "', '" & par.PulisciStrSql(par.IfNull(myReader("CANAGRAFICA"), "")) & "', '" & par.PulisciStrSql(par.IfNull(myReader("NANAGRAFICA"), "")) _
                            & "','" & myReader("DATA_APP") & Format(CInt(Replace(myReader("ORE_APP"), ".", "")), "0000") & "', '" & myReader("DATA_APP") & Format(CInt(Replace(myReader("ORE_FINE_APP"), ".", "")), "0000") _
                            & "', NULL," & par.IfNull(myReader("ID_CONTRATTO"), "NULL") & ", 0, " & par.IfNull(myReader("ID_SPORTELLO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()

                    Loop
                    myReader.Close()

                    par.cmd.CommandText = "SELECT CONVOCAZIONI_AU_GRUPPI.ID_AU,CONVOCAZIONI_AU.* FROM SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE id_contratto is null and CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU.ID_GRUPPO AND CONVOCAZIONI_AU.ID_GRUPPO=" & txtid.Value & "  order by id_sportello asc,data_app asc,to_number(replace(ore_app,'.','')) asc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        par.cmd.CommandText = "Insert into SISCOM_MI.AGENDA_APPUNTAMENTI  (ID, ID_STATO, ID_FILIALE, ID_CONVOCAZIONE, ID_AU, N_OPERATORE, COD_CONTRATTO, COGNOME, NOME, " _
                                            & "INIZIO, FINE, MOTIVAZIONE_ANNULLO, ID_CONTRATTO, TIPO_F_ORARIA, ID_SPORTELLO) Values " _
                                            & "(SISCOM_MI.SEQ_AGENDA_APPUNTAMENTI.nextval, 0, " & myReader("ID_FILIALE") & ", " & par.IfNull(myReader("ID"), "NULL") & ", " & myReader("ID_AU") & ", '" & myReader("N_OPERATORE") & "', '" _
                                            & "" & "', '', '" _
                                            & "','" & myReader("DATA_APP") & Format(CInt(Replace(myReader("ORE_APP"), ".", "")), "0000") & "', '" & myReader("DATA_APP") & Format(CInt(Replace(myReader("ORE_FINE_APP"), ".", "")), "0000") _
                                            & "', NULL,null, 0, " & par.IfNull(myReader("ID_SPORTELLO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    Loop
                    myReader.Close()
                Else
                    Response.Write("<script>alert('Attenzione...non è possibile confermare questa simulazione perchè una o più simulazioni sono state confermate nello stesso intervallo di tempo (inizio e fine simulazione) e potrebbero aver occupato degli slot definiti come liberi nella simulazione selezionata.\nGli appuntamenti di questa simulazione non sono più validi...creare una nuova simulazione specificando un intervallo di tempo differente!');</script>")
                End If



                par.myTrans.Commit()
                'par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');</script>")
                txtid.Value = ""
                eliminato.Value = "0"
                txtmia.Text = "Nessuna Selezione"
                BindGrid()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub
End Class
