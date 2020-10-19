
Partial Class ANAUT_NuovaListaManuale
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim trovato As Boolean = False
            Dim b As Boolean = False
            Dim stringaSQL As String = ""

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    trovato = True
                End If
            Next
            If trovato = False Then
                Response.Write("<script>alert('Selezionare almeno una struttura!');</script>")
                Exit Sub
            End If

            trovato = False
            For i = 0 To chListSportelli.Items.Count - 1
                If chListSportelli.Items(i).Selected = True Then
                    trovato = True
                End If
            Next
            If trovato = False Then
                Response.Write("<script>alert('Selezionare almeno uno sportello/sede!');</script>")
                Exit Sub
            End If

            If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "--" Then
                If cmbCompDa.SelectedItem.Text > cmbCompA.SelectedItem.Text Then
                    Response.Write("<script>alert('Intervallo numero componenti errato!');</script>")
                    Exit Sub
                End If
            End If


            'stringaSQL = " UTENZA_FILIALI.ID_BANDO=" & cmbAU.SelectedItem.Value
            'b = True

            Dim s As String = ""
            Dim strutture As String = ""

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    s = s & chListStrutture.Items(i).Value & ","
                    strutture = strutture & chListStrutture.Items(i).Text & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                If b = True Then
                    s = " AND ID_TAB_FILIALI IN (" & s & ")"
                Else
                    s = " ID_TAB_FILIALI IN (" & s & ")"
                End If
                b = True
            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim Sportelli As String = ""
            For i = 0 To chListSportelli.Items.Count - 1
                If chListSportelli.Items(i).Selected = True Then
                    s = s & chListSportelli.Items(i).Value & ","
                    Sportelli = Sportelli & chListSportelli.Items(i).Text & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                If b = True Then
                    s = " AND ID_SPORTELLO IN (" & s & ")"
                Else
                    s = " ID_SPORTELLO IN (" & s & ")"
                End If
                b = True
            End If

            stringaSQL = stringaSQL & s

            Dim NumComponenti As String = "Tutti"

            s = ""
            If cmbCompDa.SelectedItem.Text <> "--" Or cmbCompA.SelectedItem.Text <> "--" Then
                If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text = "--" Then
                    If b = True Then
                        s = " AND num_comp>=" & cmbCompDa.SelectedItem.Text
                    Else
                        s = " num_comp>=" & cmbCompDa.SelectedItem.Text
                    End If
                    NumComponenti = "da " & cmbCompDa.SelectedItem.Text
                End If
                If cmbCompDa.SelectedItem.Text = "--" And cmbCompA.SelectedItem.Text <> "--" Then
                    If b = True Then
                        s = " AND num_comp<=" & cmbCompA.SelectedItem.Text
                    Else
                        s = " num_comp<=" & cmbCompA.SelectedItem.Text
                    End If
                    NumComponenti = "fino a " & cmbCompA.SelectedItem.Text
                End If

                If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "--" Then
                    If b = True Then
                        s = " AND num_comp>=" & cmbCompDa.SelectedItem.Text & " AND NUM_COMP<=" & cmbCompA.SelectedItem.Text
                    Else
                        s = " num_comp>=" & cmbCompDa.SelectedItem.Text & " AND NUM_COMP<=" & cmbCompA.SelectedItem.Text
                    End If
                    NumComponenti = "Da " & cmbCompDa.SelectedItem.Text & " fino a " & cmbCompA.SelectedItem.Text
                End If

                b = True
            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim NumComponenti65 As String = "Indifferente"

            If cmb65.SelectedItem.Text = "SI" Or cmb65.SelectedItem.Text = "NO" Then
                If cmb65.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND maggiori_65>0"
                    Else
                        s = " maggiori_65>0"
                    End If
                    NumComponenti65 = "SI"
                End If

                If cmb65.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND maggiori_65=0"
                    Else
                        s = " maggiori_65=0"
                    End If
                    NumComponenti65 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim NumComponenti15 As String = "Indifferente"

            If cmb15.SelectedItem.Text = "SI" Or cmb15.SelectedItem.Text = "NO" Then
                If cmb15.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND MINORI_15>0"
                    Else
                        s = " MINORI_15>0"
                    End If
                    NumComponenti15 = "SI"
                End If

                If cmb15.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND MINORI_15=0"
                    Else
                        s = " MINORI_15=0"
                    End If
                    NumComponenti15 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim NumComponenti6699 As String = "Indifferente"

            If cmb6699.SelectedItem.Text = "SI" Or cmb6699.SelectedItem.Text = "NO" Then
                If cmb6699.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NUM_COMP_66>0"
                    Else
                        s = " NUM_COMP_66>0"
                    End If
                    NumComponenti6699 = "SI"
                End If

                If cmb6699.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NUM_COMP_66=0"
                    Else
                        s = " NUM_COMP_66=0"
                    End If
                    NumComponenti6699 = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim NumComponenti100Non As String = "Indifferente"

            If cmb100Non.SelectedItem.Text = "SI" Or cmb100Non.SelectedItem.Text = "NO" Then
                If cmb100Non.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NUM_COMP_100<>'0'"
                    Else
                        s = " NUM_COMP_100<>'0'"
                    End If
                    NumComponenti100Non = "SI"
                End If

                If cmb100Non.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NUM_COMP_100='0'"
                    Else
                        s = " NUM_COMP_100='0'"
                    End If
                    NumComponenti100Non = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim NumComponenti100Acc As String = "Indifferente"

            If cmb100Acc.SelectedItem.Text = "SI" Or cmb100Acc.SelectedItem.Text = "NO" Then
                If cmb100Acc.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND NUM_COMP_100_CON<>'0'"
                    Else
                        s = " NUM_COMP_100_CON<>'0'"
                    End If
                    NumComponenti100Acc = "SI"
                End If

                If cmb100Acc.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND NUM_COMP_100_CON='0'"
                    Else
                        s = " NUM_COMP_100_CON='0'"
                    End If
                    NumComponenti100Acc = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim RedditoDip As String = "Indifferente"

            If cmbDip.SelectedItem.Text = "SI" Or cmbDip.SelectedItem.Text = "NO" Then
                If cmbDip.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND PREVALENTE_DIPENDENTE='X'"
                    Else
                        s = " PREVALENTE_DIPENDENTE='X'"
                    End If
                    RedditoDip = "SI"
                End If

                If cmbDip.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND (PREVALENTE_DIPENDENTE IS NULL OR PREVALENTE_DIPENDENTE='') "
                    Else
                        s = " (PREVALENTE_DIPENDENTE IS NULL OR PREVALENTE_DIPENDENTE='')"
                    End If
                    RedditoDip = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim RedditoImm As String = "Indifferente"

            If cmbImmobiliare.SelectedItem.Text = "SI" Or cmbImmobiliare.SelectedItem.Text = "NO" Then
                If cmbImmobiliare.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND REDDITI_IMMOBILIARI ='X' "
                    Else
                        s = " REDDITI_IMMOBILIARI='X' "
                    End If
                    RedditoImm = "SI"
                End If

                If cmbImmobiliare.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND (REDDITI_IMMOBILIARI IS NULL OR REDDITI_IMMOBILIARI='')"
                    Else
                        s = " (REDDITI_IMMOBILIARI IS NULL OR REDDITI_IMMOBILIARI='')"
                    End If
                    RedditoImm = "NO"
                End If
                b = True

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim TUTORESTR As String = "Indifferente"
            If cmbTutore.SelectedItem.Text = "SI" Or cmbTutore.SelectedItem.Text = "NO" Then
                If cmbTutore.SelectedItem.Text = "SI" Then
                    If b = True Then
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                        TUTORESTR = "SI"
                    Else
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                        TUTORESTR = "NO"
                    End If
                End If
                If cmbTutore.SelectedItem.Text = "NO" Then
                    If b = True Then
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                        TUTORESTR = "SI"
                    Else
                        s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                        TUTORESTR = "NO"
                    End If
                End If
            End If
            stringaSQL = stringaSQL & s


            s = ""
            Dim SINDACATO As String = "Indifferente"
            If cmbSindacato.SelectedItem.Text <> "TUTTI" Then
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.SINDACATO = " & cmbSindacato.SelectedItem.Value
                    SINDACATO = cmbSindacato.SelectedItem.Text
                Else
                    s = " RAPPORTI_UTENZA.SINDACATO =" & cmbSindacato.SelectedItem.Value
                    SINDACATO = cmbSindacato.SelectedItem.Text
                End If

            End If
            stringaSQL = stringaSQL & s

            s = ""
            Dim stipula As String = ""
            If txtStipulaDal.Text <> "" Then
                stipula = "Dal " & txtStipulaDal.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s

            s = ""
            If txtStipulaAl.Text <> "" Then
                stipula = stipula & " fino al " & txtStipulaAl.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s

            If stipula = "" Then stipula = "Indifferente"

            s = ""
            Dim sloggio As String = ""
            If txtSloggioDal.Text <> "" Then
                sloggio = "Dal " & txtSloggioDal.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s

            s = ""
            If txtSloggioAl.Text <> "" Then
                sloggio = sloggio & " fino al " & txtSloggioAl.Text
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
                Else
                    s = " RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
                End If
            End If
            stringaSQL = stringaSQL & s
            If sloggio = "" Then sloggio = "Indifferente"

            s = ""
            Dim tipoC As String = ""
            Dim tipologiaC As String = "Indifferente"
            If cmbTipoContratto.SelectedItem.Text <> "TUTTI" Then
                tipologiaC = cmbTipoContratto.SelectedItem.Text
                tipoC = cmbTipoContratto.SelectedItem.Value
            End If


            stringaSQL = stringaSQL & s & " and "


            Response.Write("<script>window.open('CreazioneListaManuale.aspx?ID=" & Request.QueryString("ID") & "&Q=" & PAR.Cripta("STRUTTURE:" & strutture & "-SPORTELLI:" & Sportelli & "-Num.Comp.:" & NumComponenti & "-Maggiori 65:" & NumComponenti65 & "-minori 15:" & NumComponenti15 & "-Comp.Inv. 66-99%:" & NumComponenti6699 & "-Comp.Inv.100% No ACC.:" & NumComponenti100Non & "-Comp.Inv.100% Acc.:" & NumComponenti100Acc & "-Redd.Prev.Dip.:" & RedditoDip & "-Redd.Immob.:" & RedditoImm & "-Tutore Str.:" & TUTORESTR & "-Sindacato Rif.:" & SINDACATO & "-Stipula:" & stipula & "-Sloggio:" & sloggio & "-Tipo Rapporto:" & tipologiaC) & "&TIPOC=" & tipoC & "&IDB=" & cmbAU.SelectedItem.Value & "&S=" & PAR.Cripta(stringaSQL) & "','','');</script>")

        Catch ex As Exception

        End Try
    End Sub

    Private Function CaricaDatiAU()
        Try
            Dim trovato As Boolean = False

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE ID IN (SELECT DISTINCT id_au FROM UTENZA_LISTE_CONV) order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbAU.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), PAR.IfNull(myReader("id"), "0")))
                trovato = True
            Loop
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If trovato = False Then
                Response.Write("<script>alert('Attenzione...non è ancora stata creata nessuna lista di possibili convocabili!');</script>")
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
       
    End Function

    Private Function CaricaStrutture()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListStrutture.Items.Clear()

            PAR.cmd.CommandText = "SELECT DISTINCT filiale,ID_TAB_FILIALI FROM UTENZA_LISTE_CDETT WHERE id_lista_conv is null and motivazione IS NULL AND id_lista IN (SELECT ID from UTENZA_LISTE_CONV where id_au=" & cmbAU.SelectedItem.Value & ") order by filiale asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                chListStrutture.Items.Add(New ListItem(PAR.IfNull(myReader("FILIALE"), ""), PAR.IfNull(myReader("ID_TAB_FILIALI"), "0")))
            Loop
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        
    End Function

    Protected Sub chListStrutture_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chListStrutture.SelectedIndexChanged
        CaricaSportelli()
    End Sub

    Private Function CaricaSportelli()
        Try
            Dim i As Integer = 0
            Dim s As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListSportelli.Items.Clear()

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    s = s & chListStrutture.Items(i).Value & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                s = " WHERE ID_TAB_FILIALI IN (" & s & ")"

                'PAR.cmd.CommandText = "SELECT UTENZA_SPORTELLI.* FROM UTENZA_SPORTELLI " & s & " order by DESCRIZIONE asc"
                PAR.cmd.CommandText = "SELECT DISTINCT SEDE,ID_SPORTELLO FROM UTENZA_LISTE_CDETT " & s & " ORDER BY SEDE ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReader.Read
                    chListSportelli.Items.Add(New ListItem(PAR.IfNull(myReader("SEDE"), ""), PAR.IfNull(myReader("id_SPORTELLO"), "0")))
                Loop
                myReader.Close()

            End If



            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Function

    Protected Sub cmbAU_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAU.SelectedIndexChanged
        CaricaStrutture()
        CaricaTipoContratti()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()

            Label26.Text = "Nuova Lista Convocazione - Ricerca da liste Convocabili - Step 1"

            CaricaDatiAU()
            CaricaStrutture()
            CaricaTipoContratti()
            CaricaSindacati()
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtSloggioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtSloggioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CaricaSindacati()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbSindacato.Items.Add(New ListItem("TUTTI", "0"))
            PAR.cmd.CommandText = "SELECT * FROM sindacati_vsa"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbSindacato.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), myReader("id")))
            Loop

            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaTipoContratti()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If
            cmbTipoContratto.Items.Clear()
            cmbTipoContratto.Items.Add(New ListItem("TUTTI", "0"))
            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where id=" & cmbAU.SelectedItem.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                If PAR.IfNull(myReader("erp_1"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP SOCIALE", "1"))
                End If

                If PAR.IfNull(myReader("erp_2"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP MODERATO", "2"))
                End If

                If PAR.IfNull(myReader("erp_FF_OO"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP FF.OO.", "3"))
                End If

                If PAR.IfNull(myReader("erp_art_22"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP ART.22 C.10", "4"))
                End If

                If PAR.IfNull(myReader("erp_conv"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP CONVENZIONATO", "5"))
                End If

                If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
                End If

                If PAR.IfNull(myReader("ERP_3"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("ERP Contratti precari Art. 15 let. a, b", "7"))
                End If

                If PAR.IfNull(myReader("L39278"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("392/78", "8"))
                End If

                If PAR.IfNull(myReader("L43198"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("431/98", "9"))
                End If

                If PAR.IfNull(myReader("OA"), "") = "1" Then
                    cmbTipoContratto.Items.Add(New ListItem("Occupazioni Abusive", "10"))
                End If

            End If
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function
End Class
