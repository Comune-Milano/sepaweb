
Partial Class ANAUT_com_patrimonioI
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
        'If txtValore.Text = "" Then
        '    L2.Visible = True
        '    L2.Text = "(Valorizzare)"
        '    Exit Sub
        'Else
        '    L2.Visible = False
        'End If

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

        'If IsNumeric(txtValore.Text) = True Then
        '    L2.Visible = False
        'Else
        '    L2.Visible = True
        '    L2.Text = "(Valore Numerico)"
        '    Exit Sub
        'End If



        'If CDbl(txtValore.Text) >= 0 Then
        '    L2.Visible = False
        'Else
        '    L2.Visible = True
        '    L2.Text = "(Valore superiori o uguali a 0)"
        '    Exit Sub
        'End If

        If IsNumeric(TxtMutuo.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If



        'If cmbResidenza.SelectedItem.Text = "SI" And cmbTipo.SelectedItem.Text <> "FABBRICATI" Then
        '    L4.Visible = True
        '    L4.Text = "(SI solo se FABBRICATI)"
        'Else
        '    L4.Visible = False
        'End If



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

        'If InStr(txtValore.Text, ".") = 0 Then
        '    L2.Visible = False
        '    If InStr(txtValore.Text, ",") = 0 Then
        '        L2.Visible = False
        '    Else
        '        L2.Visible = True
        '        L2.Text = "(Valore interi)"
        '    End If
        'Else
        '    L2.Visible = True
        '    L2.Text = "(Valore interi)"
        'End If

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

        If cmbTipo.SelectedItem.Text <> "FABBRICATI" And cmbTipoImm.SelectedItem.Text <> "--" Then
            L5.Visible = True
            L5.Text = "(Indicare Cat.Castale SOLO se alloggio, altrimenti --)"
        End If

        If cmbTipo.SelectedItem.Text = "FABBRICATI" And cmbTipoImm.SelectedItem.Text = "--" Then
            L5.Visible = True
            L5.Text = "(Indicare Cat.Castale, numero vani e sup.)"
        End If

        If txtOperazione.Text = "0" Then
            If calcoloICI.Value = 0 Then
                L2.Visible = True
                L2.Text = "Valore ICI/IMU non calcolato, premere l'apposito pulsante!"
            End If
            'Else
            '    If calcoloICI.Value = 0 Then
            '        L2.Visible = True
            '        L2.Text = "Valore ICI/IMU da ricalcolare!"
            '    End If
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


        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L5.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Dim pienaproprieta As String = "  "

        If ChPiena.Checked = True Then
            pienaproprieta = Chr(160) & Chr(160) & Chr(160) & Chr(160) & "SI"
        Else
            pienaproprieta = Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160)
        End If



        'Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbTipo.SelectedItem.Text, 20) & " " & par.MiaFormat(txtPerc.Text, 6) & " " & par.MiaFormat(Val(txtValore.Text), 8) & ",00 " & par.MiaFormat(Val(TxtMutuo.Text), 8) & ",00 " & par.MiaFormat("", 2) & " " & par.MiaFormat(cmbTipoImm.SelectedItem.Text, 3) & " " & par.MiaFormat(cmbComune.SelectedItem.Text, 30) & " " & par.MiaFormat(txtNumVani.Text, 2) & " " & par.MiaFormat(txtSupUtile.Text, 7) & " " & pienaproprieta
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbTipo.SelectedItem.Text, 20) & " " & par.MiaFormat(txtPerc.Text, 7) & " " & par.MiaFormat(cmbTipoImm.SelectedItem.Text, 3) & " " & par.MiaFormat(Val(txtRendita.Text), 8) & ",00 " & " " & par.MiaFormat(Val(txtValore.Text), 8) & ",00 " & par.MiaFormat(Val(TxtMutuo.Text), 12) & ",00 " & " " & par.MiaFormat(cmbComune.SelectedItem.Text, 20) & " " & par.MiaFormat(txtNumVani.Text, 2) & " " & par.MiaFormat(txtSupUtile.Text, 7) & " " & pienaproprieta


        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub


    Private Function RiempiCatCatastale()
        par.OracleConn.Open()
        par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
        Dim lsiFrutto As New ListItem("--", "NULL")
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

        Response.Write("<script></script>")
        If Not IsPostBack Then

            RiempiCatCatastale()

            TxtMutuo.Text = par.Elimina160(Request.QueryString("MU"))
            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtPerc.Text = par.Elimina160(Request.QueryString("PER"))
            txtRendita.Text = par.Elimina160(Request.QueryString("RENDITA"))

            txtValore.Text = par.Elimina160(Request.QueryString("VAL"))

            txtNumVani.Text = par.Elimina160(Request.QueryString("VANI"))
            txtSupUtile.Text = par.Elimina160(Request.QueryString("SUP"))

            txtRendita.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            If par.Elimina160(Request.QueryString("PIE")) = "SI" Then
                ChPiena.Checked = True
            Else
                ChPiena.Checked = False
            End If




            If txtOperazione.Text = "1" Then
                'cmbResidenza.SelectedIndex = -1
                'cmbResidenza.Items.FindByText(par.Elimina160(Request.QueryString("RES"))).Selected = True
                cmbTipo.SelectedIndex = -1
                cmbTipo.Items.FindByText(par.Elimina160(UCase(Request.QueryString("TIPO")))).Selected = True
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True

                cmbComune.SelectedIndex = -1
                If Request.QueryString("COMUNE") <> "" Then
                    If Not IsNothing(cmbComune.Items.FindByText(par.Elimina160(UCase(Request.QueryString("COMUNE"))))) Then
                        cmbComune.Items.FindByText(par.Elimina160(UCase(Request.QueryString("COMUNE")))).Selected = True
                    End If
                Else
                    cmbComune.Items.FindByText(par.Elimina160(UCase("MILANO"))).Selected = True
                End If
                cmbTipoImm.SelectedIndex = -1
                If Request.QueryString("CATASTALE") <> "" Then
                    If Not IsNothing(cmbTipoImm.Items.FindByText(par.Elimina160(UCase(Request.QueryString("CATASTALE"))))) Then
                        cmbTipoImm.Items.FindByText(par.Elimina160(UCase(Request.QueryString("CATASTALE")))).Selected = True
                    End If
                Else
                    cmbTipoImm.Items.FindByText(par.Elimina160(UCase("a1"))).Selected = True
                End If


            Else
                txtValore.Text = ""
                TxtMutuo.Text = "0"
                txtPerc.Text = "100"
                txtNumVani.Text = "0"
                txtSupUtile.Text = "0"
                ChPiena.Checked = False
                cmbComune.Items.FindByText(par.Elimina160(UCase("MILANO"))).Selected = True
            End If
        End If
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

    Protected Sub btnCalcolaICI_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCalcolaICI.Click
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

    Protected Function CalcolaICI() As Double
        Dim renditaCat As Double = 0
        Dim iciImu As Double = 0
        Dim rivalutaz As Double = 0
        Dim percPropr As Double = 0


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            rivalutaz = CDbl((txtRendita.Text / 100) * 5)
            renditaCat = CDbl(txtRendita.Text) + rivalutaz

            If cmbTipo.SelectedValue = 0 Then
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
            Else
                iciImu = renditaCat
            End If

            'If txtPerc.Text <> "100" Then
            '    percPropr = (iciImu / 100) * txtPerc.Text
            '    iciImu = iciImu - percPropr
            'End If
            iciImu = (iciImu * par.PuntiInVirgole(txtPerc.Text)) / 100

            txtValore.Text = Format(iciImu, "0")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
        End Try

        Return iciImu

    End Function
End Class
