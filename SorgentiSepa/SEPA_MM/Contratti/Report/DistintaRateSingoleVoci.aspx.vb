
Partial Class Contratti_Report_DistintaRateSingoleVoci
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim S As String = ""
            Dim StringaSQL As String = "SELECT  T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE , TO_CHAR (SUM(BOL_BOLLETTE_VOCI.IMPORTO), '9G999G999G999G990D99') AS IMPORTO,'' AS DETTAGLI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.RAPPORTI_UTENZA WHERE  UNITA_IMMOBILIARI.ID=BOL_BOLLETTE.ID_UNITA AND BOL_BOLLETTE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.FL_ANNULLATA='0' AND BOL_BOLLETTE.N_RATA<>'99999' AND BOL_BOLLETTE.FL_STAMPATO='1' "

            'Dim StringaSQLAnnullate As String = "SELECT  TO_CHAR (SUM(BOL_BOLLETTE_VOCI.IMPORTO), '9G999G999G999G990D99') AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.FL_ANNULLATA<>'0' AND BOL_BOLLETTE.N_RATA<>'99999'  "
            Dim StringaSQLAnnullate As String = "select TO_CHAR(sum(BOL_BOLLETTE.IMPORTO_TOTALE), '9G999G999G999G990D99') AS IMPORTO from SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.bol_bollette,SISCOM_MI.RAPPORTI_UTENZA where RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID=BOL_BOLLETTE.ID_UNITA AND bol_bollette.fl_annullata<>'0' AND BOL_BOLLETTE.N_RATA<>'99999' "

            Dim ElencoAnnullate As String = "select bol_bollette.*,BOL_BOLLETTE.IMPORTO_TOTALE AS IMPORTO,RAPPORTI_UTENZA.COD_CONTRATTO from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA, siscom_mi.bol_bollette where UNITA_IMMOBILIARI.ID=BOL_BOLLETTE.ID_UNITA AND BOL_BOLLETTE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND bol_bollette.fl_annullata<>'0' AND BOL_BOLLETTE.N_RATA<>'99999' "


            'If par.IfEmpty(txtDataDal.Text, "Null") <> "Null" Then


            'If par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") <> "Null" Then
            '    If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
            '        Response.Write("<script>alert('Intervallo non valido!')</script>")
            '        Exit Sub
            '    End If
            'End If


            '*********SEZIONE INERENTE ALLE DATE
            If par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") <> "Null" And par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") <> "Null" Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    Response.Write("<script>alert('Intervallo date non valido (DATA EMISSIONE)!')</script>")
                    'Me.txtDataAl.Text = ""
                    'Me.txtDataDal.Text = ""
                    Exit Sub
                End If
            End If

            If par.IfEmpty(par.AggiustaData(Me.txtDataAl0.Text), "Null") <> "Null" And par.IfEmpty(par.AggiustaData(Me.txtDataDal0.Text), "Null") <> "Null" Then
                If par.AggiustaData(Me.txtDataDal0.Text) > par.AggiustaData(Me.txtDataAl0.Text) Then
                    Response.Write("<script>alert('Intervallo date non valido (DATA PERIODO BOLLETTAZIONE)!')</script>")
                    'Me.txtDataAl0.Text = ""
                    'Me.txtDataDal.Text = ""
                    Exit Sub
                End If
            End If


            If par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE>= " & par.AggiustaData(Me.txtDataDal.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_EMISSIONE>= " & par.AggiustaData(Me.txtDataDal.Text)
                ElencoAnnullate = ElencoAnnullate & " AND DATA_EMISSIONE>= " & par.AggiustaData(Me.txtDataDal.Text)
            End If

            If par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE<= " & par.AggiustaData(Me.txtDataAl.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_EMISSIONE<= " & par.AggiustaData(Me.txtDataAl.Text)
                ElencoAnnullate = ElencoAnnullate & " AND DATA_EMISSIONE<= " & par.AggiustaData(Me.txtDataAl.Text)
            End If

            If par.IfEmpty(Me.txtDataDal0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_DA>= " & par.AggiustaData(Me.txtDataDal0.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND RIFERIMENTO_DA>= " & par.AggiustaData(Me.txtDataDal0.Text)
                ElencoAnnullate = ElencoAnnullate & " AND RIFERIMENTO_DA>= " & par.AggiustaData(Me.txtDataDal0.Text)
            End If

            If par.IfEmpty(Me.txtDataAl0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_A<= " & par.AggiustaData(Me.txtDataAl0.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND RIFERIMENTO_A<= " & par.AggiustaData(Me.txtDataAl0.Text)
                ElencoAnnullate = ElencoAnnullate & " AND RIFERIMENTO_A<= " & par.AggiustaData(Me.txtDataAl0.Text)
            End If


            If Me.RdbTipologia.SelectedValue = "Attiva" Then
                StringaSQL = StringaSQL & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
                ElencoAnnullate = ElencoAnnullate & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
            End If

            If Me.RdbTipologia.SelectedValue = "Bollettazione" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE.RIF_FILE<>'FIN' AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV' and BOL_BOLLETTE.RIF_FILE<>'REC' "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.RIF_FILE<>'FIN' AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV' and BOL_BOLLETTE.RIF_FILE<>'REC' "
                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.RIF_FILE<>'FIN' AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV' and BOL_BOLLETTE.RIF_FILE<>'REC' "
            End If

            If Me.RdbTipologia.SelectedValue = "Virt.Manuale" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE.RIF_FILE='REC' "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.RIF_FILE='REC' "
                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.RIF_FILE='REC' "
            End If

            '*********FINE SEZIONE INERENTE ALLE DATE

            If chSoloUsd.Checked = True Then
                StringaSQL = StringaSQL & " AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'AL' OR (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='USD' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL') OR (SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)='CON' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL')) "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'AL' OR (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='USD' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL') OR (SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)='CON' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL')) "
                ElencoAnnullate = ElencoAnnullate & " AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'AL' OR (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='USD' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL') OR (SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)='CON' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL')) "
            End If
            If Selezionati = "1" Then
                StringaSQL = StringaSQL & " AND ("
                'StringaSQLAnnullate = StringaSQLAnnullate & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "

                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "

                Dim i As Integer = 0
                Dim primo As Boolean = False
                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        If primo = False Then
                            S = " ID_VOCE =" & item.Value
                            primo = True
                        Else
                            S = S & " OR ID_VOCE =" & item.Value
                        End If

                    End If
                Next
                StringaSQL = StringaSQL & S & ")"
                'StringaSQLAnnullate = StringaSQLAnnullate & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")  ORDER BY BOL_BOLLETTE.INTESTATARIO ASC"
                ElencoAnnullate = ElencoAnnullate & S & ")  ORDER BY BOL_BOLLETTE.INTESTATARIO ASC"
            Else

                StringaSQL = StringaSQL & " AND ("
                'StringaSQLAnnullate = StringaSQLAnnullate & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "
                ElencoAnnullate = ElencoAnnullate & " AND BOL_BOLLETTE.ID IN ( SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE "

                Dim i As Integer = 0
                Dim primo As Boolean = False

                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If primo = False Then
                        S = " ID_VOCE <>" & item.Value
                        primo = True
                    Else
                        S = S & " and ID_VOCE <>" & item.Value
                    End If

                Next
                StringaSQL = StringaSQL & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")"
                ElencoAnnullate = ElencoAnnullate & S & ") ORDER BY BOL_BOLLETTE.INTESTATARIO ASC"


            End If

            If Selezionati <> "1" Then
                Response.Write("<script>alert('Selezionare almeno una voce!');</script>")
                Exit Sub
            End If

            Session.Add("REPORT", StringaSQL)
            Session.Add("REPORT1", StringaSQLAnnullate)
            Session.Add("REPORT2", ElencoAnnullate)


            'Response.Write("<script>window.open('StampaD_SingoleVoci.aspx?O=0&DAL1=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL1=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&TIPO=" & Me.RdbTipologia.SelectedValue.ToString & "');</script>")
			Response.Write("<script>window.open('StampaD_SingoleVoci.aspx?USD=" & ChSoloUsd.Checked & "&O=0&DAL1=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL1=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&TIPO=" & Me.RdbTipologia.SelectedValue.ToString & "');</script>")            
'Me.txtDataAl.Text = ""
            'Me.txtDataDal.Text = ""
            'CaricaVociBolletta()
            'Selezionati = ""
            'Else
            'Response.Write("<script>alert('Definire il criterio di ricerca!')</script>")
            'Exit Sub

            'End If

        Catch ex As Exception
            Me.lblErrore.Visible = True

            lblErrore.Text = ex.Message

        End Try

    End Sub
    Private Sub CaricaVociBolletta()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Me.CheckVociBoll.Items.Clear()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE id<10000 ORDER BY DESCRIZIONE ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                CheckVociBoll.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        SELEZIONA()
    End Sub

    Private Function SELEZIONA()
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = False
                i = i + 1
            End While
        End If
    End Function

    Private Function SelezionaTuttoComune()

        Selezionati = 1

        Dim a As Integer
        Dim i As Integer = 0

        a = CheckVociBoll.Items.Count.ToString
        While i < a
            If Me.CheckVociBoll.Items(i).Value = 96 Or Me.CheckVociBoll.Items(i).Value = 7 Then
                Me.CheckVociBoll.Items(i).Selected = False
            Else
                Me.CheckVociBoll.Items(i).Selected = True
            End If
            i = i + 1
        End While
       
    End Function


    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaVociBolletta()
            SELEZIONA()
        End If
        txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub CheckVociBoll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckVociBoll.SelectedIndexChanged
        Selezionati = 1
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contabilita/pagina_home.aspx""</script>")

    End Sub

    Protected Sub ChComune_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChComune.CheckedChanged
        If ChComune.Checked = True Then
            SelezionaTuttoComune()
            CheckVociBoll.Enabled = False
            btnSelezionaTutto.Enabled = False
        Else
            CheckVociBoll.Enabled = True
            btnSelezionaTutto.Enabled = True
        End If
    End Sub
End Class
