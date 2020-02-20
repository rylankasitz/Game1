<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.2" tiledversion="1.3.2" name="Basic Platformer" tilewidth="20" tileheight="20" spacing="3" margin="4" tilecount="480" columns="30">
 <editorsettings>
  <export target="../../../../../../Documents/Basic Platformer.json" format="json"/>
 </editorsettings>
 <image source="../Sprites/spritesheet.png" trans="5e81a2" width="694" height="372"/>
 <tile id="1">
  <objectgroup draworder="index" id="3">
   <object id="6" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="2">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="-1.77636e-15" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="3">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="4">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="5">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="8">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="9">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="10">
  <properties>
   <property name="Name" value="Water"/>
   <property name="Trigger" type="bool" value="true"/>
  </properties>
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="11" width="20" height="9"/>
  </objectgroup>
 </tile>
 <tile id="12">
  <properties>
   <property name="Name" value="Lava"/>
   <property name="Trigger" type="bool" value="true"/>
  </properties>
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="11" width="20" height="9"/>
  </objectgroup>
 </tile>
 <tile id="13">
  <properties>
   <property name="Name" value="Lava"/>
   <property name="Trigger" type="bool" value="true"/>
  </properties>
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="11" width="20" height="9"/>
  </objectgroup>
 </tile>
 <tile id="19">
  <properties>
   <property name="Animation" value="PlayerIdle"/>
  </properties>
  <animation>
   <frame tileid="19" duration="800"/>
  </animation>
 </tile>
 <tile id="28">
  <properties>
   <property name="Animation" value="PlayerWalk"/>
   <property name="Entity" value="Player"/>
  </properties>
  <animation>
   <frame tileid="28" duration="100"/>
   <frame tileid="29" duration="100"/>
  </animation>
 </tile>
 <tile id="31">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
   <object id="2" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="32">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="33">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="34">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="35">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20"/>
  </objectgroup>
 </tile>
 <tile id="38">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="39">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="312">
  <properties>
   <property name="Name" value="Flag"/>
   <property name="Trigger" type="bool" value="true"/>
  </properties>
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="20">
    <properties>
     <property name="Name" value="Flag"/>
     <property name="Trigger" type="bool" value="true"/>
    </properties>
   </object>
  </objectgroup>
 </tile>
 <tile id="365">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="366">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="368">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="369">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="370">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="20" height="10"/>
  </objectgroup>
 </tile>
 <tile id="433">
  <properties>
   <property name="Animation" value="EnemyFly"/>
  </properties>
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="6" width="20" height="10"/>
  </objectgroup>
  <animation>
   <frame tileid="433" duration="200"/>
   <frame tileid="434" duration="200"/>
  </animation>
 </tile>
</tileset>
