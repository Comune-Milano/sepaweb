
Partial Class ANAUT_com_patrimonioI
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Function CalcolaICI() As Double
        Dim renditaCat As Double = 0
        Dim iciImu As Double = 0
        Dim rivalutaz As Double = 0
        Dim percPropr As Double = 0

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case cmbTipo.SelectedValue
                Case 0
                    rivalutaz = CDbl((txtRendita.Text / 100) * 5)
                    renditaCat = CDbl(txtRendita.Text) + rivalutaz

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_CATEGORIE_IMMOBILE WHERE COD='" & cmbTipoImm.SelectedValue & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If Request.QueryString("ANNOREDD") <> "2012" Then
                            iciImu = renditaCat * par.IfNull(myReader("COEFF_ICI"), 0)
                        Else
                            iciImu = renditaCat * par.IfNull(myReader("COEFF_IMU"), 0)
                        End If
                    End If
                    myReader.Close()
                    iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                    txtValore.Text = Format(iciImu, "0")
                Case 1
                    rivalutaz = CDbl((txtRendita.Text / 100) * 25)
                    renditaCat = CDbl(txtRendita.Text) + rivalutaz
                    iciImu = renditaCat
                    iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                    iciImu = iciImu * 75
                    txtValore.Text = Format(iciImu, "0")
                Case 2
                    renditaCat = CDbl(txtRendita.Text)
                    iciImu = renditaCat
                    iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100
                    txtValore.Text = Format(iciImu, "0")
            End Select

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'lblErrore.Text = ex.Message
        End Try

        Return iciImu

    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Dim valoreICINew As Double = 0
        Dim valoreICIOld As Double = 0

        If txtPerc.Text = "" Then
            L1.Visible = True
            L1.Text = "(Valorizzare)"
            Exit Sub
        Else
            L1.Visible = False
        End If
        If TxtMutuo.Text = "" Then
            L3.Visible = True
            L3.Text = "(Valorizzare)"
            Exit Sub
        Else
            L3.Visible = False
        End If

        If Val(TxtMutuo.Text) > Val(txtValore.Text) Then
            L3.Visible = True
            L3.Text = "(Mutuo inferiore a Valore)"
            Exit Sub
        Else
            L3.Visible = False
        End If

        If IsNumeric(txtPerc.Text) = True Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If CDbl(par.PuntiInVirgole(txtPerc.Text)) > 100 Then
            L1.Visible = True
            L1.Text = "(Valore massimo=100%)"
        Else
            L1.Visible = False
        End If

        If txtOperazione.Text = "0" Then
            If calcoloICI.Value = 0 Then
                L2.Visible = True
                L2.Text = "Valore ICI/IMU non calcolato, premere l'apposito pulsante!"
            End If
        End If

        If txtValore.Text = "" Then
            L2.Visible = True
            L2.Text = "Valore ICI/IMU non calcolato, premere l'apposito pulsante!"
        Else
            valoreICIOld = par.IfNull(txtValore.Text, 0)
            valoreICINew = CalcolaICI()

            If Format(valoreICINew, "0") <> Format(valoreICIOld, "0") Then
                txtValore.Text = ""
                L2.Visible = True
                L2.Text = "Valore ICI/IMU da ricalcolare, premere l'apposito pulsante!"
            End If
        End If

        If IsNumeric(TxtMutuo.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If CDbl(TxtMutuo.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If InStr(TxtMutuo.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(TxtMutuo.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
        End If

        L5.Visible = False

        If txtSupUtile.Text = "00" Or txtSupUtile.Text = "0,0" Or txtSupUtile.Text = "0,00" Then
            txtSupUtile.Text = "0"
        End If

        If Mid(cmbTipoImm.SelectedItem.Text, 1, 1) <> "A" And (txtNumVani.Text <> "0" Or txtSupUtile.Text <> "0") Then
            L5.Visible = True
            L5.Text = "(Inserire 0 se Cat.Castale diverso da alloggio)"
        End If

        If Mid(cmbTipoImm.SelectedItem.Text, 1, 1) = "A" And (txtNumVani.Text = "0" Or txtSupUtile.Text = "0") Then
            L5.Visible = True
            L5.Text = "(Indicare entrambi i valori se Cat.Castale = alloggio)"
        End If

        If cmbTipo.SelectedItem.Text <> "FABBRICATI" And cmbTipoImm.SelectedItem.Text <> "- - -" Then
            L5.Visible = True
            L5.Text = "(Indicare Cat.Castale SOLO se alloggio, altrimenti - - -)"
        End If

        If cmbTipo.SelectedItem.Text = "FABBRICATI" And cmbTipoImm.SelectedItem.Text = "- - -" Then
            L5.Visible = True
            L5.Text = "(Indicare Cat.Castale, numero vani e sup.)"
        End If

        If cmbTipoPropr.SelectedItem.Value = "-1" Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Attenzione...Specificare il tipo di proprietà!');", True)
            Exit Sub
        End If

        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L5.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Dim pienaproprieta As String = "  "

        If ChPiena.Checked = True Then
            pienaproprieta = Chr(160) & Chr(160) & Chr(160) & Chr(160) & "SI"
        Else
            pienaproprieta = Chr(160) & Chr(160) & Chr(160) & Chr(160) & "NO"
        End If

        If (cmbComponente.SelectedValue = "-1") Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Componente non specificato!');", True)
            Exit Sub
        End If
        ScriviPatrImmobiliare()
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Sub ScriviPatrImmobiliare()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            Dim tipoPropr As String = ""
            Dim FL_70KM As String = ""

            tipoPropr = par.PulisciStrSql(cmbTipoPropr.SelectedValue)

            If tipoPropr = "-1" Then
                tipoPropr = "NULL"
            End If

            FL_70KM = "0"
            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "'"
            Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCC.Read() Then
                If myReaderCC("DISTANZA_KM") <= 70 Then
                    FL_70KM = "1"
                End If
                If myReaderCC("DISTANZA_KM") = "0" Then
                    If cmbTipo.SelectedValue = "0" Then
                        If cmbTipoImm.SelectedValue >= 0 And cmbTipoImm.SelectedValue < 11 Then
                            Response.Write("<script>alert('Attenzione...per il comune selezionato non è stata specificata la distanza in km. Il calcolo del canone potrebbe essere ERRATO. Contattare il responsabile');</script>")
                        End If
                    End If
                End If
            End If
            myReaderCC.Close()

            If txtOperazione.Text = "0" Then
                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID, ID_COMPONENTE, ID_TIPO, PERC_PATR_IMMOBILIARE, VALORE, MUTUO, F_RESIDENZA, CAT_CATASTALE, COMUNE, N_VANI, SUP_UTILE, FL_70KM, INDIRIZZO, CIVICO, REND_CATAST_DOMINICALE, ID_TIPO_PROPRIETA,FL_VENDUTO) " _
                    & " VALUES (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & cmbComponente.SelectedValue & "," & cmbTipo.SelectedValue & "," & par.VirgoleInPunti(txtPerc.Text) & "," & txtValore.Text & "," & TxtMutuo.Text & ",'0','" & cmbTipoImm.SelectedItem.Text & "','" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "','" & txtNumVani.Text & "','" & txtSupUtile.Text & "','" & FL_70KM & "','" & par.PulisciStrSql(txtIndirizzo.Text) & "','" & txtCivico.Text & "'," & txtRendita.Text & "," & tipoPropr & "," & Valore01(chVenduto.Checked) & ") "
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID=" & txtRiga.Text
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    par.cmd.CommandText = "UPDATE UTENZA_COMP_PATR_IMMOB" _
                        & " SET " _
                        & " ID_COMPONENTE=" & cmbComponente.SelectedValue & "," _
                        & " ID_TIPO= " & cmbTipo.SelectedValue & "," _
                        & " PERC_PATR_IMMOBILIARE=" & par.VirgoleInPunti(txtPerc.Text) & "," _
                        & " VALORE= " & txtValore.Text & "," _
                        & " MUTUO=" & TxtMutuo.Text & "," _
                        & " CAT_CATASTALE='" & cmbTipoImm.SelectedItem.Text & "'," _
                        & " COMUNE='" & par.PulisciStrSql(cmbComune.SelectedItem.Text) & "'," _
                        & " N_VANI='" & txtNumVani.Text & "'," _
                        & " SUP_UTILE='" & txtSupUtile.Text & "'," _
                        & " FL_70KM='" & FL_70KM & "'," _
                        & " INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) & "'," _
                        & " CIVICO='" & txtCivico.Text & "'," _
                        & " REND_CATAST_DOMINICALE=" & txtRendita.Text & "," _
                        & " ID_TIPO_PROPRIETA=" & tipoPropr & "" _
                        & ",FL_VENDUTO=" & Valore01(chVenduto.Checked) _
                        & " WHERE ID=" & txtRiga.Text
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderI.Close()
            End If

            salvaPatrImmob.Value = "1"
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Commit()
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaPatrImmob.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviPatrMob" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Private Function RiempiCatCatastale()
        par.OracleConn.Open()




        Select cmbTipo.SelectedValue
            Case "0"
                cmbTipoImm.Enabled = True
                par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            Case "1"
                cmbTipoImm.Enabled = False
                cmbTipoImm.Items.Clear()
                'par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE WHERE DESCRIZIONE NOT LIKE 'A%' order by descrizione", "DESCRIZIONE", "COD")
            Case "2"
                cmbTipoImm.Enabled = False
                cmbTipoImm.Items.Clear()
                'par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE WHERE DESCRIZIONE NOT LIKE 'A%' order by descrizione", "DESCRIZIONE", "COD")
            Case Else
                'par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")

        End Select

        Dim lsiFrutto As New ListItem("- - -", "NULL")
        cmbTipoImm.Items.Add(lsiFrutto)
        par.RiempiDList(Me, par.OracleConn, "cmbComune", "select * from comuni_nazioni where sigla<>'E' and sigla<>'C' and sigla<>'I' order by nome asc", "NOME", "ID")
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        Response.Write("<script></script>")
        If Not IsPostBack Then

            RiempiCatCatastale()
            vIdConnessione = Request.QueryString("IDCONN")
            TxtMutuo.Text = par.Elimina160(Request.QueryString("MU"))
            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtPerc.Text = par.Elimina160(Request.QueryString("PER"))
            txtValore.Text = par.Elimina160(Request.QueryString("VAL"))

            txtNumVani.Text = par.Elimina160(Request.QueryString("VANI"))
            txtSupUtile.Text = par.Elimina160(Request.QueryString("SUP"))

           
            par.caricaComboBox("SELECT UTENZA_COMP_NUCLEO.ID,UTENZA_COMP_NUCLEO.COGNOME||' '||NOME AS NOMINATIVO FROM UTENZA_COMP_NUCLEO where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & Request.QueryString("IDDICH") & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)
            par.caricaComboBox("SELECT * FROM TIPO_PIENA_PATR_IMMOB ORDER BY ID ASC", cmbTipoPropr, "ID", "DESCRIZIONE", True)

            If par.Elimina160(Request.QueryString("PIE")) = "SI" Then
                ChPiena.Checked = True
            Else
                ChPiena.Checked = False
            End If

            txtIndirizzo.Text = par.Elimina160(Request.QueryString("IND"))
            txtCivico.Text = par.Elimina160(Request.QueryString("CIV"))

            txtRendita.Text = par.Elimina160(Request.QueryString("RENDITA"))

            txtRendita.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            TxtMutuo.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            If par.Elimina160(Request.QueryString("VEN")) = "SI" Then
                chVenduto.Checked = True
            Else
                chVenduto.Checked = False
            End If


            If txtOperazione.Text = "1" Then
                'cmbResidenza.SelectedIndex = -1
                'cmbResidenza.Items.FindByText(par.Elimina160(Request.QueryString("RES"))).Selected = True
                cmbTipo.SelectedIndex = -1
                cmbTipo.Items.FindByText(par.Elimina160(UCase(Request.QueryString("TIPO")))).Selected = True
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True

                cmbComune.SelectedIndex = -1
                If Request.QueryString("COMUNE") <> "" Then
                    cmbComune.Items.FindByText(par.Elimina160(UCase(Request.QueryString("COMUNE")))).Selected = True
                Else
                    cmbComune.Items.FindByText(par.Elimina160(UCase("MILANO"))).Selected = True
                End If
                cmbTipoImm.SelectedIndex = -1
                If Request.QueryString("CATASTALE") <> "" And Request.QueryString("CATASTALE") <> "0" Then
                    cmbTipoImm.Items.FindByText(par.Elimina160(UCase(Request.QueryString("CATASTALE")))).Selected = True
                Else
                    cmbTipoImm.Items.FindByText(par.Elimina160(UCase("a1"))).Selected = True
                End If

                If Request.QueryString("TIPOPR") <> "" Then
                    cmbTipoPropr.Items.FindByText(Request.QueryString("TIPOPR")).Selected = True
                End If
            Else
                txtValore.Text = ""
                TxtMutuo.Text = "0"
                txtPerc.Text = "100"
                txtNumVani.Text = "0"
                txtSupUtile.Text = "0"
                ChPiena.Checked = False
                cmbComune.Items.FindByText(par.Elimina160(UCase("MILANO"))).Selected = True
                txtIndirizzo.Text = ""
                txtCivico.Text = ""
            End If
        End If

        ' Dim CTRL As Control
        'For Each CTRL In Me.Controls
        '    If TypeOf CTRL Is TextBox Then
        '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is DropDownList Then
        '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is CheckBox Then
        '        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
        '    End If
        'Next

        SettaControlModifiche(Me)
    End Sub



    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
        DirectCast(cmbTipo, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Attendi').style.visibility='visible';document.getElementById('txtModificato').value='1';")
    End Sub

    Function Riempi(ByVal testo As String)
        Dim pos As Integer
        Dim Valore1 As String
        Dim Valore2 As String

        pos = 1

        Valore1 = ""
        Valore2 = ""
        Do While pos <= Len(testo)
            If Mid(testo, pos, 1) <> ";" Then
                Valore1 = Valore1 & Mid(testo, pos, 1)
            Else
                pos = pos + 1
                Do While pos <= Len(testo)
                    If Mid(testo, pos, 1) <> "!" Then
                        Valore2 = Valore2 & Mid(testo, pos, 1)
                    Else
                        cmbComponente.Items.Add(New ListItem(Valore1, Valore2))
                        Valore1 = ""
                        Valore2 = ""
                        Exit Do
                    End If
                    pos = pos + 1
                Loop
            End If
            pos = pos + 1
        Loop
    End Function

    Protected Sub btnCalcolaICI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCalcolaICI.Click
        txtValore.Text = ""

        If txtRendita.Text <> "" Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Dato Mancante)"
            Exit Sub
        End If
        If IsNumeric(txtRendita.Text) = True Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore Numerico)"
            Exit Sub
        End If
        If CDbl(txtRendita.Text) >= 0 Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If L2.Visible = False Then
            CalcolaICI()
        End If

    End Sub

    Protected Sub cmbTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        RiempiCatCatastale()
    End Sub
End Class
