<?xml version="1.0" encoding="utf-8"?>
<iso:schema   
	xmlns="http://purl.oclc.org/dsdl/schematron"
	xmlns:iso="http://purl.oclc.org/dsdl/schematron"
	queryBinding="xslt2">
	
	<iso:title>Test ISO Schematron File for DDMSence (DDMS 2.0)</iso:title>
	<iso:ns prefix='ddms' uri='http://metadata.dod.mil/mdr/ns/DDMS/2.0/' />
	<iso:ns prefix='ISM' uri='urn:us:gov:ic:ism:v2' />
	<iso:ns prefix='gml' uri='http://www.opengis.net/gml' />
	<iso:ns prefix='xlink' uri='http://www.w3.org/1999/xlink' />

	<!-- This rule employs the XPath 2.0 function, tokenize(). This nonsensical test requires 
		that every instance of gml:pos in a DDMS resource have a fixed value. -->
		
	<iso:pattern id="FGM_Reston_Location">
	   <iso:rule context="//gml:pos">
	      <iso:let name="firstCoord" value="number(tokenize(text(), ' ')[1])"/>
	      <iso:let name="secondCoord" value="number(tokenize(text(), ' ')[2])"/>
	      <iso:assert test="$firstCoord = 32.1">The first coordinate in a gml:pos element must be 32.1 degrees.</iso:assert>
	      <iso:assert test="$secondCoord = 40.2">The second coordinate in a gml:pos element must be 40.2 degrees.</iso:assert>
	   </iso:rule>
	</iso:pattern>
</iso:schema>