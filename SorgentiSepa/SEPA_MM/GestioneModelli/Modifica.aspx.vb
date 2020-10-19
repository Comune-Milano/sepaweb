Imports System.IO
Imports System.Xml


Partial Class GestioneModelli_Modifica
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If Not IsPostBack Then
            vTipo = Request.QueryString("T")
            Select Case vTipo
                Case "12"
                    LBLcONTRATTO.Text = "CONTRATTO A CANONE CONVENZIONATO"
                    ApriERPConvenzionato()
                    HiddenField1.Value = "CONV"
                Case "11"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98 SPECIALI"
                    Apri431Speciali()
                    HiddenField1.Value = "431S"
                    S4P31.Visible = False
                    S4P32.Visible = False
                    S4P33.Visible = False
                    S4P34.Visible = False
                    S4P35.Visible = False
                    S4P36.Visible = False
                    S4P37.Visible = False
                    S4P38.Visible = False
                    S4P39.Visible = False
                    S4P40.Visible = False
                    S4P41.Visible = False
                    S4P42.Visible = False
                    S4P43.Visible = False
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False
                Case "4"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98 COOPERATIVE"
                    HiddenField1.Value = "431C"
                    Apri431Cooperative()
                Case "7"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98 POR"
                    HiddenField1.Value = "431P"
                    Apri431POR()
                Case "8"
                    LBLcONTRATTO.Text = "CONTRATTO 392/78 ASSOCIAZIONI"
                    HiddenField1.Value = "392ASS"
                    Apri392ASS()
                Case "5"
                    LBLcONTRATTO.Text = "CONTRATTO USD POSTO AUTO"
                    HiddenField1.Value = "BOX"
                    ApriUSDBOX()
                Case "6"
                    LBLcONTRATTO.Text = "CONTRATTO USD NEGOZI"
                    HiddenField1.Value = "NEGOZI"
                    ApriUSDNEGOZI()
                Case "15"
                    LBLcONTRATTO.Text = "CONTRATTO USD CONCESSIONI"
                    HiddenField1.Value = "CONCESSIONE"
                    ApriUSDCONCESSIONE()
                Case "16"
                    LBLcONTRATTO.Text = "CONTRATTO USD COMODATO D'USO GRATUITO"
                    HiddenField1.Value = "COMODATO"
                    ApriUSDCOMODATO()
                Case "1"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98"
                    Apri431()
                    HiddenField1.Value = "431"
                    S4P31.Visible = False
                    S4P32.Visible = False
                    S4P33.Visible = False
                    S4P34.Visible = False
                    S4P35.Visible = False
                    S4P36.Visible = False
                    S4P37.Visible = False
                    S4P38.Visible = False
                    S4P39.Visible = False
                    S4P40.Visible = False
                    S4P41.Visible = False
                    S4P42.Visible = False
                    S4P43.Visible = False
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False

                Case "13"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98 Art.15"
                    HiddenField1.Value = "ART15"
                    ApriART15()
                Case "14"
                    LBLcONTRATTO.Text = "CONTRATTO 431/98 Art.15 c.2 bis"
                    HiddenField1.Value = "ART15_c2"
                    ApriART15_c2()
                Case "0"
                    LBLcONTRATTO.Text = "CONTRATTO ERP CANONE SOCIALE"
                    ApriERP()
                    HiddenField1.Value = "ERP"
                Case "2"
                    LBLcONTRATTO.Text = "CONTRATTO ERP CANONE MODERATO"
                    ApriERPB()
                    HiddenField1.Value = "ERPB"
                Case "3"
                    LBLcONTRATTO.Text = "CONTRATTO USI DIVERSI"
                    ApriUSD()
                    HiddenField1.Value = "USD"
                    S4P31.Visible = False
                    S4P32.Visible = False
                    S4P33.Visible = False
                    S4P34.Visible = False
                    S4P35.Visible = False
                    S4P36.Visible = False
                    S4P37.Visible = False
                    S4P38.Visible = False
                    S4P39.Visible = False
                    S4P40.Visible = False
                    S4P41.Visible = False
                    S4P42.Visible = False
                    S4P43.Visible = False
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False
            End Select
        End If
    End Sub

    Public Property vTipo() As String
        Get
            If Not (ViewState("par_vTipologia") Is Nothing) Then
                Return CStr(ViewState("par_vTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTipologia") = value
        End Set

    End Property


    Private Function ApriERPConvenzionato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=12"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=12 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P54.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P55.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P56.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P57.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P58.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P59.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P60.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Sub ApriART15()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=13"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=13 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    'S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()



            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub ApriART15_c2()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=14"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=14 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    'S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()



            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Sub
    Private Function ApriERP()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=0"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=0 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P54.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P55.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P56.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P57.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P58.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P59.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P60.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function ApriERPB()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=2"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=2 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P54.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P55.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P56.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P57.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P58.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P59.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P60.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ApriUSDBOX()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=5"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=5 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                   
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ApriUSDCONCESSIONE()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=15"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=15 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()

                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ApriUSDCOMODATO()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=16"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=16 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()

                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function ApriUSDNEGOZI()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=6"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=6 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()

                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ApriUSD()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=3"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=3 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()

                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P31.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P32.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P33.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P34.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P35.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P36.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P37.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P38.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P39.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P40.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P41.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P42.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P43.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P44.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P45.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P46.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P47.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P48.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P49.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P50.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P51.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P52.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P53.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P54.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P55.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P56.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P57.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P58.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P59.Text = par.IfNull(myReaderB("TESTO"), "")
                    'myReaderB.Read()
                    'S4P60.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function Apri392ASS()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)




            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=8"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=8 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function Apri431POR()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)




            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=7"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=7 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()





                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function Apri431Cooperative()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)




            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=4"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=4 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()





                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function Apri431Speciali()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=11"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=11 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()





                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function




    Private Function Apri431()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)




            par.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=1"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloContratto.Text = par.IfNull(myReaderA("TITOLO"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=1 ORDER BY ID ASC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                txtTitoloSezione1.Text = par.IfNull(myReaderA("TITOLO"), "")

                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S1P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S1P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione2.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S2P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S2P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione3.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S3P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S3P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()





                myReaderA.Read()
                txtTitoloSezione4.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S4P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P6.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P7.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P8.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P9.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P10.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P11.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P12.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P13.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P14.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P15.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P16.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P17.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P18.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P19.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P20.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P21.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P22.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P23.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P24.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P25.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P26.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P27.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P28.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P29.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S4P30.Text = par.IfNull(myReaderB("TESTO"), "")
                End If
                myReaderB.Close()


                myReaderA.Read()
                txtTitoloSezione5.Text = par.IfNull(myReaderA("TITOLO"), "")
                par.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & par.IfNull(myReaderA("id"), "-1") & " order by id asc"
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    S5P1.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P2.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P3.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P4.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                    S5P5.Text = par.IfNull(myReaderB("TESTO"), "")
                    myReaderB.Read()
                End If
                myReaderB.Close()
            End If
            myReaderA.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function


    Protected Sub imgEsci_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImgEsci.DataBinding
        Response.Write("<scrip>self.close();</script>")
    End Sub


    Protected Sub imgAnteprima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnteprima.Click

        Try

            'par.OracleConn.Open()
            'par.SettaCommand(par)


            Dim percorso As String = ""
            Dim NomeFile As String = "TEST_" & Format(Now, "yyyyMMddHHmmss")
            Dim TestoContratto As String = ""


            TestoContratto = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & " ?>" & vbCrLf _
                          & "<!DOCTYPE FileContratti SYSTEM " & Chr(34) & "FileContratti.dtd" & Chr(34) & ">" & vbCrLf _
                          & "<?xml-stylesheet href=" & Chr(34) & "FileContratti XLS.xsl" & Chr(34) & " type=" & Chr(34) & "text/xsl" & Chr(34) & "?>" & vbCrLf _
                          & "<FileContratti CodiceFiscaleFornitore=" & Chr(34) & "XXXXXXXXXXXXXXXX" & Chr(34) & " CodiceUfficio=" & Chr(34) & "XXX" & Chr(34) & " CodiceFiscaleConto=" & Chr(34) & "XXXXXXXXXXXXXXXX" & Chr(34) & " CodiceAzienda=" & Chr(34) & "00000" & Chr(34) & " CodiceCABSportello=" & Chr(34) & "000000" & Chr(34) & " ProvinciaBanca=" & Chr(34) & "XX" & Chr(34) & " ValutaPrelievo=" & Chr(34) & "E" & Chr(34) & ">" & vbCrLf _
                          & "<Contratto TipoContratto=" & Chr(34) & "S" & Chr(34) & " IdContratto=" & Chr(34) & "0" & Chr(34) & " SoggettoIVA=" & Chr(34) & "N" & Chr(34) & " RegistrazioneEsente=" & Chr(34) & "N" & Chr(34) & " ContrattoAgevolato=" & Chr(34) & "S" & Chr(34) & " OggettoLocazione=" & Chr(34) & "00" & Chr(34) & " TipoPagamento=" & Chr(34) & "P" & Chr(34) & " ImportoBollo=" & Chr(34) & "10,00" & Chr(34) & " ImportoRegistrazione=" & Chr(34) & "10" & Chr(34) & " ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34) & " ImportoInteressi=" & Chr(34) & Chr(34) & " NumeroPagine=" & Chr(34) & "1" & Chr(34) & ">" & vbCrLf

            'Select Case HiddenField1.Value
            '    Case "431"
            '        Schema = 1
            'End Select

            TestoContratto = TestoContratto & "<TitoloContratto>" & txtTitoloContratto.Text & "</TitoloContratto>" & vbCrLf
            'If txtTitoloSezione1.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & txtTitoloSezione1.Text & "</TitoloSezione>" & vbCrLf
            If S1P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S1P1.Text & "</Paragrafo>" & vbCrLf
            End If
            If S1P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S1P2.Text & "</Paragrafo>" & vbCrLf
            End If
            If S1P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S1P3.Text & "</Paragrafo>" & vbCrLf
            End If
            If S1P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S1P4.Text & "</Paragrafo>" & vbCrLf
            End If
            If S1P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S1P5.Text & "</Paragrafo>" & vbCrLf
            End If
            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione2.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & txtTitoloSezione2.Text & "</TitoloSezione>" & vbCrLf
            If S2P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S2P1.Text & "</Paragrafo>" & vbCrLf
            End If
            If S2P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S2P2.Text & "</Paragrafo>" & vbCrLf
            End If
            If S2P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S2P3.Text & "</Paragrafo>" & vbCrLf
            End If
            If S2P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S2P4.Text & "</Paragrafo>" & vbCrLf
            End If
            If S2P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S2P5.Text & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione3.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & txtTitoloSezione3.Text & "</TitoloSezione>" & vbCrLf

            If S3P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P1.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P2.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P3.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P4.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P5.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P6.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P6.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P7.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P7.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P8.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P8.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P9.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P9.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P10.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P10.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P11.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P11.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P12.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P12.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P13.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P13.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P14.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P14.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P15.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P15.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P16.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P16.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P17.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P17.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P18.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P18.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P19.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P19.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P20.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P20.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P21.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P21.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P22.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P22.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P23.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P23.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P24.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P24.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P25.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P25.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P26.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P26.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P27.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P27.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P28.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P28.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P29.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P29.Text & "</Paragrafo>" & vbCrLf
            End If
            If S3P30.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S3P30.Text & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione4.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & txtTitoloSezione4.Text & "</TitoloSezione>" & vbCrLf

            If S4P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P1.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P2.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P3.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P4.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P5.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P6.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P6.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P7.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P7.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P8.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P8.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P9.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P9.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P10.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P10.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P11.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P11.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P12.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P12.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P13.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P13.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P14.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P14.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P15.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P15.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P16.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P16.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P17.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P17.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P18.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P18.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P19.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P19.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P20.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P20.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P21.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P21.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P22.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P22.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P23.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P23.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P24.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P24.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P25.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P25.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P26.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P26.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P27.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P27.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P28.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P28.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P29.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P29.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P30.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P30.Text & "</Paragrafo>" & vbCrLf
            End If
            'If HiddenField1.Value = "ERP" Then
            If S4P31.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P31.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P32.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P32.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P33.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P33.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P34.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P34.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P35.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P35.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P36.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P36.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P37.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P37.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P38.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P38.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P39.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P39.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P40.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P40.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P41.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P41.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P42.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P42.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P43.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P43.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P44.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P44.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P45.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P45.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P46.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P46.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P47.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P47.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P48.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P48.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P49.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P49.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P50.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P50.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P51.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P51.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P52.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P52.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P53.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P53.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P54.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P54.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P55.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P55.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P56.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P56.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P57.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P57.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P58.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P58.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P59.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P59.Text & "</Paragrafo>" & vbCrLf
            End If
            If S4P60.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S4P60.Text & "</Paragrafo>" & vbCrLf
            End If
            'END If


            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione5.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & txtTitoloSezione5.Text & "</TitoloSezione>" & vbCrLf

            If S5P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S5P1.Text & "</Paragrafo>" & vbCrLf
            End If
            If S5P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S5P2.Text & "</Paragrafo>" & vbCrLf
            End If
            If S5P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S5P3.Text & "</Paragrafo>" & vbCrLf
            End If
            If S5P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S5P4.Text & "</Paragrafo>" & vbCrLf
            End If
            If S5P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & S5P5.Text & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            TestoContratto = TestoContratto & "</Contratto></FileContratti>"

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml", False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
            sr.WriteLine(TestoContratto)
            sr.Close()


            Dim url As String = Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml"
            'Dim url As String = Server.MapPath("..\Contratti\StampeContratti\X0002254.xml")
            'X0002254.xml()
            Dim xml As New XmlTextReader(url)

            Do While xml.Read
                If Trim(xml.ReadString) <> "" Then
                    TextBox1.Text = TextBox1.Text & xml.ReadString
                End If
            Loop


            Response.Write("<script>window.open('../ALLEGATI/CONTRATTI/StampeContratti/" & NomeFile & ".xml" & "','Contratto','');</script>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            Dim SCHEMA As Integer

            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case HiddenField1.Value
                Case "CONV"
                    SCHEMA = 12
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI SET TITOLO='" & par.PulisciStrSql(txtTitoloContratto.Text) & "' WHERE ID=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    'sezione 1
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE ID=50 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P1.Text) & "' WHERE ID=1670 AND ID_SEZIONE=50"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P2.Text) & "' WHERE ID=1672 AND ID_SEZIONE=50"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P3.Text) & "' WHERE ID=1674 AND ID_SEZIONE=50"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P4.Text) & "' WHERE ID=1676 AND ID_SEZIONE=50"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P5.Text) & "' WHERE ID=1677 AND ID_SEZIONE=50"
                    par.cmd.ExecuteNonQuery()


                    'sezione 2
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE ID=51 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P1.Text) & "' WHERE ID=1678 AND ID_SEZIONE=51"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P2.Text) & "' WHERE ID=1680 AND ID_SEZIONE=51"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P3.Text) & "' WHERE ID=1682 AND ID_SEZIONE=51"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P4.Text) & "' WHERE ID=1684 AND ID_SEZIONE=51"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P5.Text) & "' WHERE ID=1685 AND ID_SEZIONE=51"
                    par.cmd.ExecuteNonQuery()


                    'sezione 3
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE ID=52 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P1.Text) & "' WHERE ID=1686 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P2.Text) & "' WHERE ID=1688 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P3.Text) & "' WHERE ID=1690 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P4.Text) & "' WHERE ID=1692 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P5.Text) & "' WHERE ID=1694 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P6.Text) & "' WHERE ID=1696 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P7.Text) & "' WHERE ID=1698 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P8.Text) & "' WHERE ID=1700 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P9.Text) & "' WHERE ID=1702 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P10.Text) & "' WHERE ID=1704 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P11.Text) & "' WHERE ID=1706 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P12.Text) & "' WHERE ID=1708 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P13.Text) & "' WHERE ID=1710 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P14.Text) & "' WHERE ID=1712 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P15.Text) & "' WHERE ID=1714 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P16.Text) & "' WHERE ID=1716 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P17.Text) & "' WHERE ID=1718 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P18.Text) & "' WHERE ID=1720 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P19.Text) & "' WHERE ID=1722 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P20.Text) & "' WHERE ID=1724 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P21.Text) & "' WHERE ID=1726 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P22.Text) & "' WHERE ID=1728 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P23.Text) & "' WHERE ID=1730 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P24.Text) & "' WHERE ID=1732 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P25.Text) & "' WHERE ID=1734 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P26.Text) & "' WHERE ID=1736 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P27.Text) & "' WHERE ID=1738 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P28.Text) & "' WHERE ID=1740 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P29.Text) & "' WHERE ID=1742 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P30.Text) & "' WHERE ID=1744 AND ID_SEZIONE=52"
                    par.cmd.ExecuteNonQuery()


                    'sezione 4
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE ID=53 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P1.Text) & "' WHERE ID=1746 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P2.Text) & "' WHERE ID=1748 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P3.Text) & "' WHERE ID=1750 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P4.Text) & "' WHERE ID=1752 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P5.Text) & "' WHERE ID=1754 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P6.Text) & "' WHERE ID=1756 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P7.Text) & "' WHERE ID=1758 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P8.Text) & "' WHERE ID=1760 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P9.Text) & "' WHERE ID=1762 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P10.Text) & "' WHERE ID=1764 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P11.Text) & "' WHERE ID=1766 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P12.Text) & "' WHERE ID=1768 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P13.Text) & "' WHERE ID=1770 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P14.Text) & "' WHERE ID=1772 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P15.Text) & "' WHERE ID=1774 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P16.Text) & "' WHERE ID=1776 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P17.Text) & "' WHERE ID=1778 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P18.Text) & "' WHERE ID=1780 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P19.Text) & "' WHERE ID=1782 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P20.Text) & "' WHERE ID=1784 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P21.Text) & "' WHERE ID=1786 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P22.Text) & "' WHERE ID=1788 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P23.Text) & "' WHERE ID=1790 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P24.Text) & "' WHERE ID=1792 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P25.Text) & "' WHERE ID=1794 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P26.Text) & "' WHERE ID=1796 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P27.Text) & "' WHERE ID=1798 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P28.Text) & "' WHERE ID=1800 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P29.Text) & "' WHERE ID=1802 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P30.Text) & "' WHERE ID=1804 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P31.Text) & "' WHERE ID=1806 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P32.Text) & "' WHERE ID=1808 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P33.Text) & "' WHERE ID=1810 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P34.Text) & "' WHERE ID=1812 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P35.Text) & "' WHERE ID=1814 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P36.Text) & "' WHERE ID=1816 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P37.Text) & "' WHERE ID=1818 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P38.Text) & "' WHERE ID=1820 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P39.Text) & "' WHERE ID=1822 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P40.Text) & "' WHERE ID=1824 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P41.Text) & "' WHERE ID=1826 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P42.Text) & "' WHERE ID=1828 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P43.Text) & "' WHERE ID=1830 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P44.Text) & "' WHERE ID=1832 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P45.Text) & "' WHERE ID=1834 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P46.Text) & "' WHERE ID=1836 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P47.Text) & "' WHERE ID=1838 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P48.Text) & "' WHERE ID=1840 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P49.Text) & "' WHERE ID=1842 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P50.Text) & "' WHERE ID=1844 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P51.Text) & "' WHERE ID=1846 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P52.Text) & "' WHERE ID=1848 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P53.Text) & "' WHERE ID=1850 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P54.Text) & "' WHERE ID=1852 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P55.Text) & "' WHERE ID=1854 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P56.Text) & "' WHERE ID=1856 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P57.Text) & "' WHERE ID=1858 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P58.Text) & "' WHERE ID=1860 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P59.Text) & "' WHERE ID=1862 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P60.Text) & "' WHERE ID=1864 AND ID_SEZIONE=53"
                    par.cmd.ExecuteNonQuery()

                    'sezione 5

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE ID=54 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P1.Text) & "' WHERE ID=1866 AND ID_SEZIONE=54"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P2.Text) & "' WHERE ID=1868 AND ID_SEZIONE=54"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P3.Text) & "' WHERE ID=1870 AND ID_SEZIONE=54"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P4.Text) & "' WHERE ID=1872 AND ID_SEZIONE=54"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P5.Text) & "' WHERE ID=1874 AND ID_SEZIONE=54"
                    par.cmd.ExecuteNonQuery()
                Case "BOX"
                    SalvaSchema(5, 20, 750)

                Case "ERPB"
                    SalvaSchema(2, 30, 1050)
                    
                Case "NEGOZI"
                    SalvaSchema(6, 25, 900)
                    
                Case "392ASS"
                    SalvaSchema(8, 40, 1350)
                    
                Case "431C"
                    SalvaSchema(4, 15, 600)
                    
                Case "ART15"
                    SCHEMA = 13
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI SET TITOLO='" & par.PulisciStrSql(txtTitoloContratto.Text) & "' WHERE ID=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    'sezione 1
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE ID=55 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P1.Text) & "' WHERE ID=1876 AND ID_SEZIONE=55"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P2.Text) & "' WHERE ID=1878 AND ID_SEZIONE=55"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P3.Text) & "' WHERE ID=1880 AND ID_SEZIONE=55"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P4.Text) & "' WHERE ID=1882 AND ID_SEZIONE=55"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P5.Text) & "' WHERE ID=1884 AND ID_SEZIONE=55"
                    par.cmd.ExecuteNonQuery()


                    'sezione 2
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE ID=56 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P1.Text) & "' WHERE ID=1886 AND ID_SEZIONE=56"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P2.Text) & "' WHERE ID=1888 AND ID_SEZIONE=56"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P3.Text) & "' WHERE ID=1890 AND ID_SEZIONE=56"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P4.Text) & "' WHERE ID=1892 AND ID_SEZIONE=56"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P5.Text) & "' WHERE ID=1894 AND ID_SEZIONE=56"
                    par.cmd.ExecuteNonQuery()


                    'sezione 3
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE ID=57 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P1.Text) & "' WHERE ID=1896 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P2.Text) & "' WHERE ID=1898 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P3.Text) & "' WHERE ID=1900 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P4.Text) & "' WHERE ID=1902 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P5.Text) & "' WHERE ID=1904 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P6.Text) & "' WHERE ID=1906 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P7.Text) & "' WHERE ID=1908 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P8.Text) & "' WHERE ID=1910 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P9.Text) & "' WHERE ID=1912 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P10.Text) & "' WHERE ID=1914 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P11.Text) & "' WHERE ID=1916 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P12.Text) & "' WHERE ID=1918 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P13.Text) & "' WHERE ID=1920 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P14.Text) & "' WHERE ID=1922 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P15.Text) & "' WHERE ID=1924 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P16.Text) & "' WHERE ID=1926 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P17.Text) & "' WHERE ID=1928 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P18.Text) & "' WHERE ID=1930 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P19.Text) & "' WHERE ID=1932 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P20.Text) & "' WHERE ID=1934 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P21.Text) & "' WHERE ID=1936 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P22.Text) & "' WHERE ID=1938 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P23.Text) & "' WHERE ID=1940 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P24.Text) & "' WHERE ID=1942 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P25.Text) & "' WHERE ID=1944 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P26.Text) & "' WHERE ID=1946 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P27.Text) & "' WHERE ID=1948 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P28.Text) & "' WHERE ID=1950 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P29.Text) & "' WHERE ID=1952 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P30.Text) & "' WHERE ID=1954 AND ID_SEZIONE=57"
                    par.cmd.ExecuteNonQuery()


                    'sezione 4
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE ID=58 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P1.Text) & "' WHERE ID=1956 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P2.Text) & "' WHERE ID=1958 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P3.Text) & "' WHERE ID=1960 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P4.Text) & "' WHERE ID=1962 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P5.Text) & "' WHERE ID=1964 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P6.Text) & "' WHERE ID=1966 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P7.Text) & "' WHERE ID=1968 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P8.Text) & "' WHERE ID=1970 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P9.Text) & "' WHERE ID=1972 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P10.Text) & "' WHERE ID=1974 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P11.Text) & "' WHERE ID=1976 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P12.Text) & "' WHERE ID=1978 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P13.Text) & "' WHERE ID=1980 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P14.Text) & "' WHERE ID=1982 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P15.Text) & "' WHERE ID=1984 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P16.Text) & "' WHERE ID=1986 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P17.Text) & "' WHERE ID=1988 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P18.Text) & "' WHERE ID=1990 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P19.Text) & "' WHERE ID=1992 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P20.Text) & "' WHERE ID=1994 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P21.Text) & "' WHERE ID=1996 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P22.Text) & "' WHERE ID=1998 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P23.Text) & "' WHERE ID=2000 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P24.Text) & "' WHERE ID=2002 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P25.Text) & "' WHERE ID=2004 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P26.Text) & "' WHERE ID=2006 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P27.Text) & "' WHERE ID=2008 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P28.Text) & "' WHERE ID=2010 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P29.Text) & "' WHERE ID=2012 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P30.Text) & "' WHERE ID=2014 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P31.Text) & "' WHERE ID=2016 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P32.Text) & "' WHERE ID=2018 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P33.Text) & "' WHERE ID=2020 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P34.Text) & "' WHERE ID=2022 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P35.Text) & "' WHERE ID=2024 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P36.Text) & "' WHERE ID=2026 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P37.Text) & "' WHERE ID=2028 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P38.Text) & "' WHERE ID=2030 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P39.Text) & "' WHERE ID=2032 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P40.Text) & "' WHERE ID=2034 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P41.Text) & "' WHERE ID=2036 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P42.Text) & "' WHERE ID=2038 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P43.Text) & "' WHERE ID=2040 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P44.Text) & "' WHERE ID=2042 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P45.Text) & "' WHERE ID=2044 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P46.Text) & "' WHERE ID=2046 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P47.Text) & "' WHERE ID=2048 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P48.Text) & "' WHERE ID=2050 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P49.Text) & "' WHERE ID=2052 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P50.Text) & "' WHERE ID=2054 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P51.Text) & "' WHERE ID=2056 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P52.Text) & "' WHERE ID=2058 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P53.Text) & "' WHERE ID=2060 AND ID_SEZIONE=58"
                    par.cmd.ExecuteNonQuery()

                    'sezione 5

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE ID=59 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P1.Text) & "' WHERE ID=2076 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P2.Text) & "' WHERE ID=2078 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P3.Text) & "' WHERE ID=2080 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P4.Text) & "' WHERE ID=2082 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P5.Text) & "' WHERE ID=2084 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()
                    '*** 28/10/2013
                Case "ART15_c2"
                    SCHEMA = 14
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI SET TITOLO='" & par.PulisciStrSql(txtTitoloContratto.Text) & "' WHERE ID=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    'sezione 1
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE ID=59 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P1.Text) & "' WHERE ID=2076 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P2.Text) & "' WHERE ID=2078 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P3.Text) & "' WHERE ID=2080 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P4.Text) & "' WHERE ID=2082 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P5.Text) & "' WHERE ID=2084 AND ID_SEZIONE=59"
                    par.cmd.ExecuteNonQuery()


                    'sezione 2
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE ID=60 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P1.Text) & "' WHERE ID=2086 AND ID_SEZIONE=60"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P2.Text) & "' WHERE ID=2088 AND ID_SEZIONE=60"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P3.Text) & "' WHERE ID=2090 AND ID_SEZIONE=60"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P4.Text) & "' WHERE ID=2092 AND ID_SEZIONE=60"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P5.Text) & "' WHERE ID=2094 AND ID_SEZIONE=60"
                    par.cmd.ExecuteNonQuery()


                    'sezione 3
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE ID=61 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P1.Text) & "' WHERE ID=2096 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P2.Text) & "' WHERE ID=2098 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P3.Text) & "' WHERE ID=2100 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P4.Text) & "' WHERE ID=2102 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P5.Text) & "' WHERE ID=2104 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P6.Text) & "' WHERE ID=2106 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P7.Text) & "' WHERE ID=2108 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P8.Text) & "' WHERE ID=2110 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P9.Text) & "' WHERE ID=2112 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P10.Text) & "' WHERE ID=2114 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P11.Text) & "' WHERE ID=2116 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P12.Text) & "' WHERE ID=2118 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P13.Text) & "' WHERE ID=2120 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P14.Text) & "' WHERE ID=2122 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P15.Text) & "' WHERE ID=2124 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P16.Text) & "' WHERE ID=2126 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P17.Text) & "' WHERE ID=2128 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P18.Text) & "' WHERE ID=2130 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P19.Text) & "' WHERE ID=2132 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P20.Text) & "' WHERE ID=2134 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P21.Text) & "' WHERE ID=2136 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P22.Text) & "' WHERE ID=2138 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P23.Text) & "' WHERE ID=2140 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P24.Text) & "' WHERE ID=2142 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P25.Text) & "' WHERE ID=2144 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P26.Text) & "' WHERE ID=2146 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P27.Text) & "' WHERE ID=2148 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P28.Text) & "' WHERE ID=2150 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P29.Text) & "' WHERE ID=2152 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P30.Text) & "' WHERE ID=2154 AND ID_SEZIONE=61"
                    par.cmd.ExecuteNonQuery()


                    'sezione 4
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE ID=62 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P1.Text) & "' WHERE ID=2156 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P2.Text) & "' WHERE ID=2158 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P3.Text) & "' WHERE ID=2160 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P4.Text) & "' WHERE ID=2162 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P5.Text) & "' WHERE ID=2164 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P6.Text) & "' WHERE ID=2166 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P7.Text) & "' WHERE ID=2168 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P8.Text) & "' WHERE ID=2170 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P9.Text) & "' WHERE ID=2172 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P10.Text) & "' WHERE ID=2174 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P11.Text) & "' WHERE ID=2176 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P12.Text) & "' WHERE ID=2178 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P13.Text) & "' WHERE ID=2180 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P14.Text) & "' WHERE ID=2182 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P15.Text) & "' WHERE ID=2184 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P16.Text) & "' WHERE ID=2186 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P17.Text) & "' WHERE ID=2188 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P18.Text) & "' WHERE ID=2190 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P19.Text) & "' WHERE ID=2192 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P20.Text) & "' WHERE ID=2194 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P21.Text) & "' WHERE ID=2196 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P22.Text) & "' WHERE ID=2198 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P23.Text) & "' WHERE ID=2200 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P24.Text) & "' WHERE ID=2202 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P25.Text) & "' WHERE ID=2204 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P26.Text) & "' WHERE ID=2206 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P27.Text) & "' WHERE ID=2208 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P28.Text) & "' WHERE ID=2210 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P29.Text) & "' WHERE ID=2212 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P30.Text) & "' WHERE ID=2214 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P31.Text) & "' WHERE ID=2216 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P32.Text) & "' WHERE ID=2218 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P33.Text) & "' WHERE ID=2220 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P34.Text) & "' WHERE ID=2222 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P35.Text) & "' WHERE ID=2224 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P36.Text) & "' WHERE ID=2226 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P37.Text) & "' WHERE ID=2228 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P38.Text) & "' WHERE ID=2230 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P39.Text) & "' WHERE ID=2232 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P40.Text) & "' WHERE ID=2234 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P41.Text) & "' WHERE ID=2236 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P42.Text) & "' WHERE ID=2238 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P43.Text) & "' WHERE ID=2240 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P44.Text) & "' WHERE ID=2242 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P45.Text) & "' WHERE ID=2244 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P46.Text) & "' WHERE ID=2246 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P47.Text) & "' WHERE ID=2248 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P48.Text) & "' WHERE ID=2250 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P49.Text) & "' WHERE ID=2252 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P50.Text) & "' WHERE ID=2254 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P51.Text) & "' WHERE ID=2256 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P52.Text) & "' WHERE ID=2258 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P53.Text) & "' WHERE ID=2260 AND ID_SEZIONE=62"
                    par.cmd.ExecuteNonQuery()

                    'sezione 5

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE ID=63 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P1.Text) & "' WHERE ID=2276 AND ID_SEZIONE=63"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P2.Text) & "' WHERE ID=2278 AND ID_SEZIONE=63"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P3.Text) & "' WHERE ID=2280 AND ID_SEZIONE=63"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P4.Text) & "' WHERE ID=2282 AND ID_SEZIONE=63"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P5.Text) & "' WHERE ID=2284 AND ID_SEZIONE=63"
                    par.cmd.ExecuteNonQuery()

                Case "ERP"
                    SCHEMA = 0
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI SET TITOLO='" & par.PulisciStrSql(txtTitoloContratto.Text) & "' WHERE ID=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    'sezione 1
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE ID=5 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P1.Text) & "' WHERE ID=150 AND ID_SEZIONE=5"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P2.Text) & "' WHERE ID=152 AND ID_SEZIONE=5"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P3.Text) & "' WHERE ID=154 AND ID_SEZIONE=5"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P4.Text) & "' WHERE ID=156 AND ID_SEZIONE=5"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S1P5.Text) & "' WHERE ID=158 AND ID_SEZIONE=5"
                    par.cmd.ExecuteNonQuery()


                    'sezione 2
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE ID=6 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P1.Text) & "' WHERE ID=160 AND ID_SEZIONE=6"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P2.Text) & "' WHERE ID=162 AND ID_SEZIONE=6"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P3.Text) & "' WHERE ID=164 AND ID_SEZIONE=6"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P4.Text) & "' WHERE ID=166 AND ID_SEZIONE=6"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S2P5.Text) & "' WHERE ID=168 AND ID_SEZIONE=6"
                    par.cmd.ExecuteNonQuery()


                    'sezione 3
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE ID=7 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P1.Text) & "' WHERE ID=170 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P2.Text) & "' WHERE ID=172 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P3.Text) & "' WHERE ID=174 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P4.Text) & "' WHERE ID=176 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P5.Text) & "' WHERE ID=178 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P6.Text) & "' WHERE ID=180 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P7.Text) & "' WHERE ID=182 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P8.Text) & "' WHERE ID=184 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P9.Text) & "' WHERE ID=186 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P10.Text) & "' WHERE ID=188 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P11.Text) & "' WHERE ID=190 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P12.Text) & "' WHERE ID=192 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P13.Text) & "' WHERE ID=194 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P14.Text) & "' WHERE ID=196 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P15.Text) & "' WHERE ID=198 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P16.Text) & "' WHERE ID=200 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P17.Text) & "' WHERE ID=202 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P18.Text) & "' WHERE ID=204 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P19.Text) & "' WHERE ID=206 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P20.Text) & "' WHERE ID=208 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P21.Text) & "' WHERE ID=210 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P22.Text) & "' WHERE ID=212 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P23.Text) & "' WHERE ID=214 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P24.Text) & "' WHERE ID=216 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P25.Text) & "' WHERE ID=218 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P26.Text) & "' WHERE ID=220 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P27.Text) & "' WHERE ID=222 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P28.Text) & "' WHERE ID=224 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P29.Text) & "' WHERE ID=226 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S3P30.Text) & "' WHERE ID=228 AND ID_SEZIONE=7"
                    par.cmd.ExecuteNonQuery()


                    'sezione 4
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE ID=8 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P1.Text) & "' WHERE ID=230 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P2.Text) & "' WHERE ID=232 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P3.Text) & "' WHERE ID=234 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P4.Text) & "' WHERE ID=236 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P5.Text) & "' WHERE ID=238 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P6.Text) & "' WHERE ID=240 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P7.Text) & "' WHERE ID=242 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P8.Text) & "' WHERE ID=244 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P9.Text) & "' WHERE ID=246 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P10.Text) & "' WHERE ID=248 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P11.Text) & "' WHERE ID=250 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P12.Text) & "' WHERE ID=252 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P13.Text) & "' WHERE ID=254 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P14.Text) & "' WHERE ID=256 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P15.Text) & "' WHERE ID=258 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P16.Text) & "' WHERE ID=260 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P17.Text) & "' WHERE ID=262 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P18.Text) & "' WHERE ID=264 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P19.Text) & "' WHERE ID=266 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P20.Text) & "' WHERE ID=268 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P21.Text) & "' WHERE ID=270 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P22.Text) & "' WHERE ID=272 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P23.Text) & "' WHERE ID=274 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P24.Text) & "' WHERE ID=276 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P25.Text) & "' WHERE ID=278 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P26.Text) & "' WHERE ID=280 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P27.Text) & "' WHERE ID=282 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P28.Text) & "' WHERE ID=284 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P29.Text) & "' WHERE ID=286 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P30.Text) & "' WHERE ID=288 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P31.Text) & "' WHERE ID=290 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P32.Text) & "' WHERE ID=292 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P33.Text) & "' WHERE ID=294 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P34.Text) & "' WHERE ID=296 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P35.Text) & "' WHERE ID=298 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P36.Text) & "' WHERE ID=300 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P37.Text) & "' WHERE ID=302 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P38.Text) & "' WHERE ID=304 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P39.Text) & "' WHERE ID=306 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P40.Text) & "' WHERE ID=308 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P41.Text) & "' WHERE ID=310 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P42.Text) & "' WHERE ID=312 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P43.Text) & "' WHERE ID=314 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P44.Text) & "' WHERE ID=316 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P45.Text) & "' WHERE ID=318 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P46.Text) & "' WHERE ID=320 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P47.Text) & "' WHERE ID=322 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P48.Text) & "' WHERE ID=324 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P49.Text) & "' WHERE ID=326 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P50.Text) & "' WHERE ID=328 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P51.Text) & "' WHERE ID=330 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P52.Text) & "' WHERE ID=332 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P53.Text) & "' WHERE ID=334 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P54.Text) & "' WHERE ID=336 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P55.Text) & "' WHERE ID=338 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P56.Text) & "' WHERE ID=340 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P57.Text) & "' WHERE ID=342 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P58.Text) & "' WHERE ID=344 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P59.Text) & "' WHERE ID=346 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S4P60.Text) & "' WHERE ID=348 AND ID_SEZIONE=8"
                    par.cmd.ExecuteNonQuery()

                    'sezione 5

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE ID=9 AND ID_SCHEMA=" & SCHEMA
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P1.Text) & "' WHERE ID=350 AND ID_SEZIONE=9"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P2.Text) & "' WHERE ID=352 AND ID_SEZIONE=9"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P3.Text) & "' WHERE ID=354 AND ID_SEZIONE=9"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P4.Text) & "' WHERE ID=356 AND ID_SEZIONE=9"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(S5P5.Text) & "' WHERE ID=358 AND ID_SEZIONE=9"
                    par.cmd.ExecuteNonQuery()
                Case "431"
                    SalvaSchema(1, 0, 0)
                    
                Case "USD"
                    SalvaSchema(3, 10, 370)
                    
                Case "431P"
                    SalvaSchema(7, 35, 1200)
                    
                Case "431S"
                    SalvaSchema(11, 45, 1520)

                Case "CONCESSIONE"
                    SalvaSchema(15, 65, 2290)
                    
                Case "COMODATO"
                    SalvaSchema(16, 70, 2440)
                    
            End Select
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione Effettuata!');</script>")




        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function SalvaSchema(ByVal Schema As Integer, Sezione As Integer, Paragrafo As Integer)

        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI SET TITOLO='" & par.PulisciStrSql(txtTitoloContratto.Text) & "' WHERE ID=" & Schema
        par.cmd.ExecuteNonQuery()

        'sezione 1
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE ID=" & Sezione & " AND ID_SCHEMA=" & Schema
        par.cmd.ExecuteNonQuery()

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S1P1.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S1P2.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S1P3.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S1P4.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S1P5.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        'sezione 2
        Sezione = Sezione + 1
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE ID=" & Sezione & " AND ID_SCHEMA=" & Schema
        par.cmd.ExecuteNonQuery()

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S2P1.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S2P2.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S2P3.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S2P4.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S2P5.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        'sezione 3
        Sezione = Sezione + 1
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE ID=" & Sezione & " AND ID_SCHEMA=" & Schema
        par.cmd.ExecuteNonQuery()

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P1.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P2.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P3.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P4.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P5.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P6.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P7.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P8.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P9.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P10.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P11.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P12.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P13.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P14.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P15.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P16.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P17.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P18.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P19.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P20.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P21.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P22.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P23.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P24.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P25.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P26.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P27.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P28.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P29.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S3P30.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2


        'sezione 4
        Sezione = Sezione + 1
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE ID=" & Sezione & " AND ID_SCHEMA=" & Schema
        par.cmd.ExecuteNonQuery()

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P1.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P2.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P3.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P4.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P5.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P6.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P7.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P8.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P9.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P10.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P11.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P12.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P13.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P14.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P15.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P16.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P17.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P18.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P19.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P20.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P21.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P22.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P23.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P24.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P25.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P26.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P27.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P28.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P29.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S4P30.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        'sezione 5
        Sezione = Sezione + 1
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_SEZIONI SET TITOLO='" & par.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE ID=" & Sezione & " AND ID_SCHEMA=" & Schema
        par.cmd.ExecuteNonQuery()

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S5P1.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S5P2.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S5P3.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S5P4.Text, Paragrafo, Sezione)
        Paragrafo = Paragrafo + 2

        Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(S5P5.Text, Paragrafo, Sezione)
    End Function

    Private Function Aggiorna_SCHEMA_CONTRATTI_PARAGRAFI(ByVal Testo As String, ByVal Paragrafo As Integer, ByVal Sezione As Integer)
        par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CONTRATTI_PARAGRAFI SET TESTO='" & par.PulisciStrSql(Testo) & "' WHERE ID=" & Paragrafo & " AND ID_SEZIONE=" & Sezione
        par.cmd.ExecuteNonQuery()
    End Function
End Class
