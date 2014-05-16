<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
      <ddms:resource xsi:schemaLocation="urn:us:mil:ces:metadata:ddms:4 ../schemas/4.1/DDMS/ddms.xsd"
	        xmlns:ddms="urn:us:mil:ces:metadata:ddms:4"
	        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	        xmlns:ISM="urn:us:gov:ic:ism"
	        xmlns:ntk="urn:us:gov:ic:ntk"
	        ntk:DESVersion="7"
	        ISM:resourceElement="true" ISM:DESVersion="9" ISM:createDate="2010-01-21" ISM:classification="U" ISM:ownerProducer="USA">
        <ddms:metacardInfo ISM:classification="{librarycard/cardLayer/libcard:security.classification}" ISM:ownerProducer="USA">
          <ddms:identifier ddms:qualifier="URI" ddms:value="urn:buri:ddmsence:testIdentifier" />
          <ddms:dates ddms:created="2011-09-24" />
          <ddms:publisher>
            <ddms:person>
              <ddms:name>Brian</ddms:name>
              <ddms:surname>Uri</ddms:surname>
            </ddms:person>
          </ddms:publisher>
        </ddms:metacardInfo>
      </ddms:resource>
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>
