# Research Assessment Program
###  1. Introduction
    The University of Tasmania requires the Windows Research Assessment Program (RAP). This desktop application will be used by administrative staff in the Office of Research Services to monitor and assess the research performance of staff and research students at the University using publically available information about their employment and publication outputs. The information to be presented includes, but is not limited to, a staff member’s position, past job titles, primary campus, students they are supervising and publications they have authored, as well as derived information about their performance relative to benchmarks for their level of employment. Similar information is presented about research students, although their performance is measured differently. The information, which includes both textual data and staff photos, is to be presented via a Windows 10 desktop application.
###  2. Existing Systems
    Currently some of this information is available via the University’s Web Access Research Portal (WARP), available at:
      https://rmdb.research.utas.edu.au/public/rmdb/q/warp_home
      These are three examples of the information about staff available on the WARP site that the new system will provide, but present differently:
      https://rmdb.research.utas.edu.au/public/rmdb/q/indiv_detail_warp_trans/47832
      https://rmdb.research.utas.edu.au/public/rmdb/q/indiv_detail_warp_trans/6
      https://rmdb.research.utas.edu.au/public/rmdb/q/indiv_detail_warp_trans/4479
    The new RAP desktop application will not reproduce the layout of WARP.
    There also currently exist several administration systems that allow administrative staff to record employment and publication information. This project does not include the redevelopment of these administration systems.
###  3. Platform
    The Research Assessment Program must operate within the Standard Operating Environment (SOE) of University computers, which are a mixture of Dell and Apple products that all run Windows 10. The information that the system will display is currently stored in a MySQL database, named the “Employment and Research Database”, and this database content and structure cannot be changed. As this system does not include redevelopment of the administrative entering system that facilitates input of the information into the database, the new system must work with the existing database. Details of the database will be provided in a later document.
    The final system is to be developed using C# and the sources shared with technical staff from Information Technology Resources so that they may maintain it into the future.
###  4. Users
    The system will be used by professional staff from the Office of Research Services as well as senior administrative (academic) staff in the University. Both groups will have access to the same features, so there will be no need to differentiate between users.
###  5. Components
    The Research Assessment Program will consist of two main components: researcher lists (incorporating a researcher detail view) and publications lists (for a given researcher). These may be enhanced by the addition of a report generation function.
